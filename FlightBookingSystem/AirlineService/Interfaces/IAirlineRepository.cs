﻿using CommonDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Interfaces
{
    public interface IAirlineRepository
    {
        

        public int AddFlightDetails(TblFlight[] inventoryDetails);
    }
}
