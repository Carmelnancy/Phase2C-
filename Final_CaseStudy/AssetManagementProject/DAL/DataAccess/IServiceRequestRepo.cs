using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
    public interface IServiceRequestRepo
    {
        Task<ServiceRequest> AddServiceRequestAsync(ServiceRequest request);
        Task<IEnumerable<ServiceRequest>> GetAllRequestsAsync();
        Task<IEnumerable<ServiceRequest>> GetRequestsByEmployeeIdAsync(int employeeId);
        Task<ServiceRequest> GetRequestByIdAsync(int id);
        Task<ServiceRequest> UpdateRequestStatusAsync(int id, string newStatus);
        Task<bool> DeleteRequestAsync(int id);

    }
}
