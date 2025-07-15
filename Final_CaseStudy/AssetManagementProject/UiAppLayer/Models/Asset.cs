using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UiAppLayer.Models
{
    public class Asset
    {
       
        public int AssetId { get; set; }

     
        public string AssetNo { get; set; }

        public string AssetName { get; set; }

        public string AssetModel { get; set; }

     
        public DateTime ManufacturingDate { get; set; }

  
        public DateTime ExpiryDate { get; set; }

    
        public decimal AssetValue { get; set; }


        public string Status { get; set; }


        public int CategoryId { get; set; }


        public int? EmployeeId { get; set; }


        public int Quantity { get; set; }
    }
}
