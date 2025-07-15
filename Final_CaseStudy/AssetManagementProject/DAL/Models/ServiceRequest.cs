using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("ServiceRequests")]
    public class ServiceRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ServiceRequestId")]
        public int ServiceRequestId { get; set; }

        [Required(ErrorMessage = "Request date is required.")]
        [DataType(DataType.Date)]
        [Column("RequestDate")]
        public DateTime RequestDate { get; set; }

        [Required(ErrorMessage = "Asset ID is required.")]
        [Column("AssetId")]
        public int AssetId { get; set; }

        [Column("EmployeeId")]
        [Required]
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "Description is required.")]
        [Column("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Issue type is required.")]
        [Column("IssueType")]
        public string IssueType { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [Column("Status")]
        public string Status { get; set; }
    }

}


