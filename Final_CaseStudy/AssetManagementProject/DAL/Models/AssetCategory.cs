using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("AssetCategories")]
    public class AssetCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [Column("CategoryName")]
        public string CategoryName { get; set; }

        [Column("Description")]
        public string? Description { get; set; }
    }

}
