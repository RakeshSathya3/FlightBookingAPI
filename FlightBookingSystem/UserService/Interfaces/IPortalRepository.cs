using CommonDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
    public interface IPortalRepository
    {
        public List<string> Login(TblUser userLogin);
        public int RegisterUser(TblUser userDetails);
    }
}
