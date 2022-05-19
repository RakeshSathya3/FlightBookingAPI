using AirlineService.Interfaces;
using CommonDAL.Models;
using CommonDAL.Repositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AirlineService.Models
{
    public class AirlineRepository : IAirlineRepository, IConsumer<InventoryChangeDetails>
    {
        FlightApplicationDBContext _context;

        public AirlineRepository(FlightApplicationDBContext context)
        {
            _context = context;
        }

        public int RegisterUser(TblUser userDetails)
        {
            using (SHA512 sha512hash = SHA512.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(userDetails.Password);
                byte[] hashBytes = sha512hash.ComputeHash(sourceBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                userDetails.Password = hashedPassword;
            }

            userDetails.IsActive = "Y";
            userDetails.CreatedBy = userDetails.UserName.ToString();
            userDetails.ModifiedBy = userDetails.UserName.ToString();

            _context.TblUsers.Add(userDetails);
            int IsSuccess = _context.SaveChanges();

            return IsSuccess;
        }


        public int AddFlightDetails(TblFlight[] inventoryDetails)
        {
            int IsSuccess = 0;
            foreach (var item in inventoryDetails)
            {
                item.IsActive = "Y";
                item.CreatedBy = item.ModifiedBy = "Admin";
                item.Remarks = "Added By Admin";

                _context.TblFlights.Add(item);
                IsSuccess = _context.SaveChanges();
            }
            

            return IsSuccess;
        }

        public Task Consume(ConsumeContext<InventoryChangeDetails> context)
        {
            string FlightNo = context.Message.FlightNo;
            int NoOfBC_Seats = context.Message.NoOfBusinessClassSeats;
            int NoOfNonBC_Seats = context.Message.NoOfNonBusinessClassSeats;
            DateTime DepartureDateTime = context.Message.DepartureDateTime;

            TblFlight flightDetails = _context.TblFlights
                .FirstOrDefault(m => m.FlightNo == context.Message.FlightNo
                && m.DepartureDateTime == context.Message.DepartureDateTime 
                && m.IsActive == "Y");

            if (context.Message.Action == "Book")
            {
                
                flightDetails.NoOfBusinessClassSeats = flightDetails.NoOfBusinessClassSeats - NoOfBC_Seats;
                flightDetails.NoOfNonBusinessClassSeats = flightDetails.NoOfNonBusinessClassSeats - NoOfNonBC_Seats;
                
                _context.SaveChanges();
            }
            else if (context.Message.Action == "Cancel")
            {
                flightDetails.NoOfBusinessClassSeats = flightDetails.NoOfBusinessClassSeats + NoOfBC_Seats;
                flightDetails.NoOfNonBusinessClassSeats = flightDetails.NoOfNonBusinessClassSeats + NoOfNonBC_Seats;
                _context.TblFlights.Add(flightDetails);
                _context.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
