using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataAccess;
using DAL.Model;

namespace AppUI
{
    public class EventBL
    {
        private readonly IEventDetailsRepo _repo;
        public EventBL(IEventDetailsRepo repo) { _repo = repo; }

        public void AddEvent(EventDetails e) => _repo.Add(e);
        public List<EventDetails> GetAllEvents() => _repo.GetAll();
    }
}
