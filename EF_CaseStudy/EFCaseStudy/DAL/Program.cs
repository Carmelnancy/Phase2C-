using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataAccess;
using Microsoft.Extensions.Logging;
using DAL.Model;

namespace DAL
{
    internal class Program
    {
        static void Main(string[] args)
        {

            EventDetailsRepo repo = new EventDetailsRepo();
            repo.Add(new EventDetails { EventName = "Pongal", EventCategory = "Function", EventDate = DateTime.Parse("2025-06-11"), Description = "Its a function for celebrating pongal", Status = "Active" });
        }
    }
}
