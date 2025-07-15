using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("AssetRequests")]
    public class AssetRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RequestId")]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "Request date is required.")]
        [DataType(DataType.Date)]
        [Column("RequestDate")]
        public DateTime RequestDate { get; set; }

        [Required(ErrorMessage = "Asset ID is required.")]
        [Column("AssetId")]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "Employee ID is required.")]
        [Column("EmployeeId")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [Column("Status")]
        public string Status { get; set; }
    }

}
