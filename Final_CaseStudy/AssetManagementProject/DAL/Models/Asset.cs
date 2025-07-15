using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Assets")]
    public class Asset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("AssetId")]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "Asset number is required.")]
        [Column("AssetNo")]
        public string AssetNo { get; set; }

        [Required(ErrorMessage = "Asset name is required.")]
        [Column("AssetName")]
        public string AssetName { get; set; }

        [Required(ErrorMessage = "Asset model is required.")]
        [Column("AssetModel")]
        public string AssetModel { get; set; }

        [Required(ErrorMessage = "Manufacturing date is required.")]
        [DataType(DataType.Date)]
        [Column("ManufacturingDate")]
        public DateTime ManufacturingDate { get; set; }

        [Required(ErrorMessage = "Expiry date is required.")]
        [DataType(DataType.Date)]
        [Column("ExpiryDate")]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Asset value is required.")]
        [DataType(DataType.Currency)]
        [Column("AssetValue", TypeName = "decimal(18,2)")] 
        public decimal AssetValue { get; set; }


        [Required(ErrorMessage = "Status is required.")]
        [Column("Status")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Column("EmployeeId")]
        public int? EmployeeId { get; set; }

        [Required]
        [Column("Quantity")]
        public int Quantity { get; set; }
    }

}

