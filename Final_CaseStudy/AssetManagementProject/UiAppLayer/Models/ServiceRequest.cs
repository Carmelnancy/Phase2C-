using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UiAppLayer.Models
{
    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }


        public DateTime RequestDate { get; set; }

     
        public int AssetId { get; set; }

    
        public int EmployeeId { get; set; }



        public string Description { get; set; }

      
        public string IssueType { get; set; }

       
        public string Status { get; set; }
    }
}

