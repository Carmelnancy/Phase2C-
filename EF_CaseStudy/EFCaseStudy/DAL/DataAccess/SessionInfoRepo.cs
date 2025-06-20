using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.DataAccess
{
    public class SessionInfoRepo : ISessionInfoRepo
    {
        private readonly EventContext _context = new EventContext();


        public List<SessionInfo> GetSessionsByEventId(int eventId)
        {
            return _context.Sessions.Where(s => s.EventId == eventId).ToList();
        }
    }
}
