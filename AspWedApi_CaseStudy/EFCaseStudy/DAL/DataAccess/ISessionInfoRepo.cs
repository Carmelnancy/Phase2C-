using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.DataAccess
{
    public interface ISessionInfoRepo<T>
    {
        List<SessionInfo> GetSessionsByEventId(int eventId);
    }
}
