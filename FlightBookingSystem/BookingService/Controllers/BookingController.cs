using BookingService.Interfaces;
using BookingService.Models;
using CommonDAL.Models;
using CommonDAL.Repositories;
using MassTransit.KafkaIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Author: Rakesh S
/// Purpose: Manages Flight bookings - Book a flight, get booking history for a user, delete ticket for a user
/// </summary>
namespace BookingService.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        IBookFlightsRepository _context;
        FlightApplicationDBContext _dbContext;
        private ITopicProducer<InventoryChangeDetails> _topicProducer;

        public BookingController(IBookFlightsRepository context, FlightApplicationDBContext dbContext, ITopicProducer<InventoryChangeDetails> topicProducer)
        {
            _context = context;
            _dbContext = dbContext;
            _topicProducer = topicProducer;
        }

        [HttpPost]
        public IActionResult BookFlight(BookingInputDetails[] bookingInputDetails)
        {
            try
            {
                
                //To seperate for BC and Non BC seats
                string PNR = _context.BookFlights(bookingInputDetails);

                //await _topicProducer.Produce(new InventoryChangeDetails
                //{
                //    FlightNo = bookingInputDetails.FirstOrDefault().FlightNo,
                //    DepartureDateTime = bookingInputDetails.FirstOrDefault().DepartureDateTime,                   
                //    NoOfBusinessClassSeats = bookingInputDetails.FirstOrDefault().TblPassengers.Length,
                //    NoOfNonBusinessClassSeats = bookingInputDetails.FirstOrDefault().TblPassengers.Length,
                //    Action = "Book"
                //});

                return Ok(new { response = "Flight Booked Successfully with PNR No: " + PNR });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Response = "Error", ResponseMessage = ex.Message });
            }
        }

        //Gets all bookings for a User
        [HttpGet("history/{emailId}")]
        public IActionResult GetBookingHistory(string emailId)
        {
            try
            {
                IEnumerable<TblUser> userMaster = _dbContext.TblUsers.ToList().Where(m => m.EmailId == emailId);
                IEnumerable<TblBooking> bookingDetails = _dbContext.TblBookings.ToList().Where(m => m.UserId == userMaster.FirstOrDefault().UserId);
                IEnumerable<TblPassenger> userBookingDetails = _dbContext.TblPassengers.ToList().Where(m => m.Pnr == bookingDetails.FirstOrDefault().Pnr);

                var result = (from p in userBookingDetails
                              join t in bookingDetails on p.Pnr equals t.Pnr
                              join c in userMaster on t.UserId equals c.UserId
                              where c.EmailId == emailId
                              select new
                              {
                                  t.Pnr,
                                  c.UserName,
                                  t.FlightNo,
                                  p.PassengerName,
                                  p.PassengerAge,
                                  p.PassengerGender,
                                  p.IsMealOpted,
                                  p.MealType,
                                  t.DepartureDateTime,
                                  t.IsOneWay,
                                  t.ArrivalDateTime,
                                  t.NoOfPassengers,
                                  p.Price,
                                  p.StatusCode
                              }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Response = "Error", ResponseMessage = ex.Message });
            }
        }

        //Gets booking details for the entered PNR
        [HttpGet("ticket/{pnr}")]
        public IActionResult GetBookingDetails(int pnr)
        {
            try
            {
                TblBooking details = _dbContext.TblBookings.ToList().Find(m => m.Pnr == pnr);

                if (details != null)
                {
                    IEnumerable<TblBooking> bookingDetails = _dbContext.TblBookings.ToList().Where(m => m.Pnr == pnr);
                    IEnumerable<TblPassenger> userBookingDetails = _dbContext.TblPassengers.ToList().Where(m => m.Pnr == pnr);
                    IEnumerable<TblUser> userMaster = _dbContext.TblUsers.ToList().Where(m => m.UserId == bookingDetails.FirstOrDefault().UserId);

                    var result = (from p in userBookingDetails
                                  join t in bookingDetails on p.Pnr equals t.Pnr
                                  join c in userMaster on t.UserId equals c.UserId
                                  where t.Pnr == pnr && t.StatusCode == 1
                                  select new
                                  {
                                      t.Pnr,
                                      c.UserName,
                                      t.FlightNo,
                                      p.PassengerName,
                                      p.PassengerAge,
                                      p.PassengerGender,
                                      p.IsMealOpted,
                                      p.MealType,
                                      t.DepartureDateTime,
                                      t.IsOneWay,
                                      t.ArrivalDateTime,
                                      t.NoOfPassengers,
                                      p.Price,
                                      p.StatusCode
                                  }).ToList();
                    return Ok(result);
                }

                return Ok("No records found with the entered PNR number. Please enter the correct PNR number.");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Response = "Error",
                    ResponseMessage = ex.Message
                });
            }
        }

        [HttpDelete("cancel/{pnr}")]        
        public IActionResult CancelBooking(int pnr)
        {
            try
            {
                bool IsBookingCancelled = _context.CancelBooking(pnr);                

                if (IsBookingCancelled)
                {
                    var message = "Booking for PNR No: " + pnr + " is cancelled successfully";
                    return Accepted(message);
                }
                else
                {
                    return Ok("No records found with PNR: " + pnr);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Response = "Error",
                    ResponseMessage = ex.Message
                });
            }
        }
    }
}
