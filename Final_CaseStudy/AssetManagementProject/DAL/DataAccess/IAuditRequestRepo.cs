using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
    public interface IAuditRequestRepo
    {
        Task<AuditRequest> AddAuditRequestAsync(AuditRequest request);
        Task<IEnumerable<AuditRequest>> GetAllRequestsAsync();
        Task<IEnumerable<AuditRequest>> GetRequestsByEmployeeIdAsync(int employeeId);
        Task<AuditRequest> GetRequestByIdAsync(int id);
        Task<AuditRequest> UpdateRequestStatusAsync(int id, string newStatus);
        Task<bool> DeleteRequestAsync(int id);
       

    }
}
