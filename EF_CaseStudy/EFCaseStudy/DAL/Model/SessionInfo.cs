using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class SessionInfo
    {
        [Key]
        public int SessionId { get; set; }

        [Required]
        //[ForeignKey("EventDetails")]
        public int EventId { get; set; }

        [Required, StringLength(50)]
        public string SessionTitle { get; set; }

        //[ForeignKey("SpeakerDetails")]
        public int? SpeakerId { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime SessionStart { get; set; }

        [Required]
        public DateTime SessionEnd { get; set; }

        public string SessionUrl { get; set; }

        public EventDetails EventDetails { get; set; }
    }
}
