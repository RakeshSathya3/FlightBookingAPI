using UserService.Interfaces;
using CommonDAL.Interfaces;
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
/// Author: Rakesh S
/// Purpose: Login into the application
/// </summary>
namespace UserService.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IPortalRepository _context;
        private readonly IJWTManagerRepository _jWTManager;
        public LoginController(IPortalRepository context, IJWTManagerRepository jWTManager)
        {
            _context = context;
            _jWTManager = jWTManager;
        }

        [HttpPost("login")]
        public IActionResult Login(TblUser userLogin)
        {
            try
            {
                List<string> result = _context.Login(userLogin);

                if (result.Count == 0)
                {
                    return Ok("Incorrect Email Id/ Password");
                }

                var token = _jWTManager.Authenticate(userLogin);

                if (token == null)
                {
                    return Unauthorized();
                }

                Dictionary<string, string> lst = new Dictionary<string, string>();

                lst.Add("userId", result[0].ToString());
                lst.Add("roleId", result[1].ToString());
                lst.Add("token", token.Token);
                lst.Add("refreshToken", token.RefreshToken);

                return Ok(lst);
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
