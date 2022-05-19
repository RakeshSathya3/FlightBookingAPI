using CommonDAL.Models;
using BookingService.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class SearchFlightsRepository : ISearchFlightsRepository
    {
        FlightApplicationDBContext _context;

        public SearchFlightsRepository(FlightApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<TblFlight> SearchFlights(SearchDetails searchDetails)
        {
            //string departureDate = DateTime.ParseExact(searchDetails.DepartureDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            IEnumerable<TblFlight> searchResults = _context.TblFlights.ToList()
                                                        .Where(m => m.FromLocation == searchDetails.FromLocation
                                                                 && m.ToLocation == searchDetails.ToLocation
                                                                 && m.DepartureDateTime.ToString("yyyy-MM-dd") == searchDetails.DepartureDate
                                                                 && m.IsActive == "Y");


           
            return searchResults;
        }
    }
}
