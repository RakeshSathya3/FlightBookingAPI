using AirlineService.Interfaces;
using AirlineService.Models;
//using Common;
using CommonDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Controllers
{
    [Route("api/airline/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        IAirlineRepository _context;

        public InventoryController(IAirlineRepository context)
        {
            _context = context;
        }

        
        [HttpPost("add")]
        public IActionResult AddFlightDetails(TblFlight[] inventoryDetails)
        {
            try
            {
                int isFlightAddedSuccessfully = _context.AddFlightDetails(inventoryDetails);

                if (isFlightAddedSuccessfully > 0)
                {
                    return Ok("Flight details added successfully");
                }
                else
                {
                    return Ok("Flight details could not be added");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Response = "Error", ResponseMessage = ex.Message });
            }
        }
    }
}
