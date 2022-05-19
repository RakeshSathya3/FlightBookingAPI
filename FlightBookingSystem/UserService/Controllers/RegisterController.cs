using UserService.Interfaces;
using UserService.Models;
using CommonDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/// <summary>

/// </summary>
namespace UserService.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        IPortalRepository _context;

        public RegisterController(IPortalRepository context)
        {
            _context = context;
        }

        [HttpPost("register")]        
        public IActionResult RegisterUser(TblUser userDetails)
        {
            try
            {
                int IsRegisteredSuccessfully = _context.RegisterUser(userDetails);

                if (IsRegisteredSuccessfully > 0)
                {
                    return Ok(new { response = "User Registered successfully" });
                }
                else
                {
                    return Ok(new { response = "User could not be registered" });
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
