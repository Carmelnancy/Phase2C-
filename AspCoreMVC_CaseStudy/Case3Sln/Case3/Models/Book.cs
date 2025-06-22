using System.ComponentModel.DataAnnotations;
using Case3.Utilities;  

namespace Case3.Models
{
    public class Book
    {
        [Required(ErrorMessage = "ISBN is required")]
        public int Isbn { get; set; }

        [Required(ErrorMessage = "Book name is required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Book name must be between 1 and 20 characters")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Author name must be between 1 and 50 characters")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Published date is required")]
        [NoFutureDate(ErrorMessage = "Published date cannot be a future date")]
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "Book URL is required")]
        [Url(ErrorMessage = "Invalid URL format")]
        public string BookUrl { get; set; }
    }
}
