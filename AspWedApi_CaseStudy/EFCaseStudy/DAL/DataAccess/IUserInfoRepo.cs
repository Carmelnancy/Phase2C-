using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.DataAccess
{
    public interface IUserInfoRepo
    {
        void Add(UserInfo user);
        UserInfo ValidateUser(string email, string password);
    }
}
