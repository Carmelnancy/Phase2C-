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

                Console.WriteLine("1. Add Event\n2. View Events\n3. View Sessions by EventId");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var newEvent = new EventDetails { EventName = "AI Workshop", EventCategory = "Tech", EventDate = DateTime.Now, Status = "Active" };
                        eventBL.AddEvent(newEvent);
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
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid credentials");
            }
        }
    }
}
