using DAL.DataAccess;
using DAL.Model;

namespace AppUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            IUserInfoRepo userRepo = new UserInfoRepo();
            UserBL userBL = new UserBL(userRepo);

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (userBL.Login(email, password))
            {
                Console.WriteLine("Login Successful");

                IEventDetailsRepo eventRepo = new EventDetailsRepo();
                EventBL eventBL = new EventBL(eventRepo);

                ISessionInfoRepo sessionRepo = new SessionInfoRepo();
                SessionBL sessionBL = new SessionBL(sessionRepo);
                bool x=true;
                while (x)
                {
                    Console.WriteLine("1. Add Event\n2. View Events\n3. View Sessions by EventId\n4. Return");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Enter Event Name:");
                            string eventName = Console.ReadLine();

                            Console.WriteLine("Enter Event Category:");
                            string eventCategory = Console.ReadLine();

                            Console.WriteLine("Enter Event Date (yyyy-MM-dd):");
                            DateTime eventDate = DateTime.Parse(Console.ReadLine());

                            Console.WriteLine("Enter Description:");
                            string description = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(description))
                                description = "No description provided"; // Prevent nulls if DB doesn't allow

                            Console.WriteLine("Enter Status (Active/In-Active):");
                            string status = Console.ReadLine();

                            EventDetails newEvent = new EventDetails
                            {
                                EventName = eventName,
                                EventCategory = eventCategory,
                                EventDate = eventDate,
                                Description = description,
                                Status = status
                            };
                            eventBL.AddEvent(newEvent);
                            Console.WriteLine("Event added successfully!");
                            break;
                        case "2":
                            var events = eventBL.GetAllEvents();
                            foreach (var e in events)
                                Console.WriteLine($"{e.EventId} {e.EventName} {e.Status}");
                            break;
                        case "3":
                            Console.Write("Enter EventId: ");
                            int id = int.Parse(Console.ReadLine());
                            var sessions = sessionBL.GetSessions(id);
                            foreach (var s in sessions)
                                Console.WriteLine($"{s.SessionId} {s.SessionTitle} {s.SessionStart}");
                            break;
                        case "4":
                            Console.WriteLine("Returning");
                            x = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid credentials");
            }
            Console.ReadKey();
        }
    }
}
