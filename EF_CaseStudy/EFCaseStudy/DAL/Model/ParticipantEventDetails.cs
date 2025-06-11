using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class ParticipantEventDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ParticipantEmailId { get; set; }

        //[ForeignKey("ParticipantEmailId")]
        public UserInfo User { get; set; }

        [Required]
        public int EventId { get; set; }

        //[ForeignKey("EventId")]
        public EventDetails Event { get; set; }

        [RegularExpression("Yes|No")]
        public string IsAttended { get; set; }
    }

}
