using CommonDAL.Models;
using BookingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Interfaces
{
    public interface ISearchFlightsRepository
    {
        public IEnumerable<TblFlight> SearchFlights(SearchDetails searchDetails);
    }
}
