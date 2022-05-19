using System;
using System.Collections.Generic;

#nullable disable

namespace CommonDAL.Models
{
    public partial class TblFlight
    {
        public string FlightNo { get; set; }
        public string FlightName { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public decimal PriceBc { get; set; }
        public int NoOfBusinessClassSeats { get; set; }
        public decimal PriceNonBc { get; set; }
        public int NoOfNonBusinessClassSeats { get; set; }
        public string MealOption { get; set; }
        public string IsActive { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
