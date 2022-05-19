using UserService.Interfaces;
using CommonDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class PortalRepository : IPortalRepository
    {
        FlightApplicationDBContext _context;

        public PortalRepository(FlightApplicationDBContext context)
        {
            _context = context;
        }

        public List<string> Login(TblUser userLogin)
        {
            using (SHA512 sha512hash = SHA512.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(userLogin.Password);
                byte[] hashBytes = sha512hash.ComputeHash(sourceBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                userLogin.Password = hashedPassword;
            }

            IEnumerable<TblUser> searchResults = _context.TblUsers.ToList()
                .Where(m => m.EmailId == userLogin.EmailId && m.Password == userLogin.Password);

            List<string> lst = new List<string>();
            //Check if the entered credentials are found in the DB
            if (searchResults.ToList().Count != 0)
            {
                lst.Add(searchResults.FirstOrDefault().UserId.ToString());
                lst.Add(searchResults.FirstOrDefault().RoleId.ToString());

            }

            return lst;
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
    }
}
