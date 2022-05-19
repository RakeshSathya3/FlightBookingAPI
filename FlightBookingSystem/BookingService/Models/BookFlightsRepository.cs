using BookingService.Interfaces;
using CommonDAL.Models;
using CommonDAL.Repositories;
using MassTransit.KafkaIntegration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class BookFlightsRepository : IBookFlightsRepository
    {
        FlightApplicationDBContext _context;
        private ITopicProducer<InventoryChangeDetails> _topicProducer;

        public BookFlightsRepository(FlightApplicationDBContext context, ITopicProducer<InventoryChangeDetails> topicProducer)
        {
            _context = context;
            _topicProducer = topicProducer;
        }
        public string BookFlights(BookingInputDetails[] bookingInputDetails)
        {
            //Insert data into TblBookings table
            TblBooking bookingDetail = new TblBooking();

            foreach(var itemBookingInputDetails in bookingInputDetails)
            {
                bookingDetail.UserId = itemBookingInputDetails.UserId;
                bookingDetail.FlightNo = itemBookingInputDetails.FlightNo;
                bookingDetail.NoOfPassengers = itemBookingInputDetails.NoOfPassengers;
                bookingDetail.DepartureDateTime = itemBookingInputDetails.DepartureDateTime;
                bookingDetail.IsOneWay = itemBookingInputDetails.IsOneWay;
                bookingDetail.ArrivalDateTime = itemBookingInputDetails.ReturnDateTime;
                bookingDetail.StatusCode = 1;
                bookingDetail.CreatedBy = bookingDetail.ModifiedBy = itemBookingInputDetails.UserId.ToString();

                _context.TblBookings.Add(bookingDetail);
                _context.SaveChanges();

                //Insert data into TblPassengers table (Includes passenger wise details)
                if (itemBookingInputDetails.TblPassengers != null)
                {
                    foreach (var item in itemBookingInputDetails.TblPassengers)
                    {
                        item.Pnr = bookingDetail.Pnr;
                        item.StatusCode = 1;
                        item.CreatedBy = item.ModifiedBy = itemBookingInputDetails.UserId.ToString();

                        _context.TblPassengers.Add(item);
                        _context.SaveChanges();
                    }
                }
               
            }

            return bookingDetail.Pnr.ToString();
        }
       
        public bool CancelBooking(int pnr)
        {
            var resultBookingDetails = _context.TblBookings.Where(m => m.Pnr == pnr);           

            var resultUserBookingDetails = _context.TblPassengers.Where(m => m.Pnr == pnr);           

            if (resultBookingDetails.ToList().Count == 0 || resultUserBookingDetails.ToList().Count == 0)
            {
                return false;
            }
            else
            {

                foreach (var item in resultBookingDetails)
                {
                    item.StatusCode = 0;

                }
                foreach (var item in resultUserBookingDetails)
                {
                    item.StatusCode = 0;

                }
                _context.SaveChanges();
                return true;
            }
        }
    }
}
