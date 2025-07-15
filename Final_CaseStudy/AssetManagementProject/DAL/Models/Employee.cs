using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("EmployeeId")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
        [Column("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Column("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        [Column("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [Column("Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [Column("ContactNumber")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [Column("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [Column("Role")]
        public string Role { get; set; }
    }


}
