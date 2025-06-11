using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataAccess;

namespace AppUI
{
    public class UserBL
    {
        private readonly IUserInfoRepo _repo;
        public UserBL(IUserInfoRepo repo) { _repo = repo; }

        public bool Login(string email, string password)
        {
            return _repo.ValidateUser(email, password) != null;
        }
    }

}
