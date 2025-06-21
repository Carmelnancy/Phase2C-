namespace Case4.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; } // Admin, Trainer, Learner
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
