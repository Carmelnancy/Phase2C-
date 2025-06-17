using System.ComponentModel.DataAnnotations;

namespace ContactCaseStudy.Models
{
    public class ContactInfo
    {
        public int ContactId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string CompanyName { get; set; }

        [EmailAddress]
        public string EmailId { get; set; }

        public long MobileNo { get; set; }

        public string Designation { get; set; }
    }
}
