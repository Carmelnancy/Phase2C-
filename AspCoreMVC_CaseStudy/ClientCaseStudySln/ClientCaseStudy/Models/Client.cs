using Microsoft.AspNetCore.Mvc;

namespace ClientCaseStudy.Models
{
    public class Client
    {

        public int ClientId { get; set; }
        public string CompanyName { get; set; }
        public string WebUrl { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public string Standard { get; set; }
    }
}
