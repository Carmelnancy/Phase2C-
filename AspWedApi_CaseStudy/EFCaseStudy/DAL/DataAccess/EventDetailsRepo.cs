using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.DataAccess
{
    public class EventDetailsRepo : IEventDetailsRepo<EventDetails>
    {
        private readonly EventContext _context = new EventContext();

        public void Add(EventDetails e)
        {
            _context.Events.Add(e);
            _context.SaveChanges();
        }

        public List<EventDetails> GetAll()
        {
            return _context.Events.ToList();
        }
    }
}
