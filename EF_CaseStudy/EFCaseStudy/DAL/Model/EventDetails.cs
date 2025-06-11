using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class EventDetails
    {
        [Key]
        public int EventId { get; set; }

        [Required, StringLength(50)]
        public string EventName { get; set; }

        [Required, StringLength(50)]
        public string EventCategory { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; } // Active or In-Active
    }
}
