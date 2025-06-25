using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.DataAccess
{
    public class UserInfoRepo : IUserInfoRepo
    {
        private readonly EventContext _context = new EventContext();

        public void Add(UserInfo user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        //using(EventContext _context=new EventContext()){

        //}
        public UserInfo ValidateUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.EmailId == email && u.Password == password);
        }
    }
}
