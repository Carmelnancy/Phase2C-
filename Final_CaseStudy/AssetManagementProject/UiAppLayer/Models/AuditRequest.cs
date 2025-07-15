using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UiAppLayer.Models
{
    public class AuditRequest
    {
        public int AuditRequestId { get; set; }

    
        public DateTime RequestDate { get; set; }

     
        public int AssetId { get; set; }

   
        public int EmployeeId { get; set; }

        public string ? Status { get; set; }
    }
}
