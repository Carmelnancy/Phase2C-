using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataAccess;
using DAL.Model;

namespace AppUI
{
    public class SessionBL
    {
        private readonly ISessionInfoRepo _repo;
        public SessionBL(ISessionInfoRepo repo) { _repo = repo; }

        public List<SessionInfo> GetSessions(int eventId) => _repo.GetSessionsByEventId(eventId);
    }
}
