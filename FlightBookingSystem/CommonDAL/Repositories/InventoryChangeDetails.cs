using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonDAL.Repositories
{
    public class InventoryChangeDetails
    {

        public string FlightNo { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public int NoOfBusinessClassSeats { get; set; }
        public int NoOfNonBusinessClassSeats { get; set; }
        public string Action { get; set; }


    }
}
