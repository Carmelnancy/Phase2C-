using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class UserInfo
    {
        [Key]
        public string EmailId { get; set; }

        [Required, StringLength(50, MinimumLength = 1)]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; } // Admin, Participant

        [Required, StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }

    //public class UserInfo
    //{
    //    [Key]
    //    [EmailAddress]
    //    public string EmailId { get; set; }

    //    [Required]
    //    [StringLength(50, MinimumLength = 1)]
    //    public string UserName { get; set; }

    //    [Required]
    //    [RegularExpression("Admin|Participant")]
    //    public string Role { get; set; }

    //    [Required]
    //    [StringLength(20, MinimumLength = 6)]
    //    public string Password { get; set; }
    //}
}
