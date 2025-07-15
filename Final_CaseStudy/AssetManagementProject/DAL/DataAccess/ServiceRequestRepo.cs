using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataAccess
{
    public class ServiceRequestRepo : IServiceRequestRepo
    {
        public async Task<ServiceRequest> AddServiceRequestAsync(ServiceRequest request)
        {
            using (var context = new DatabaseContext())
            {
                int maxId = context.ServiceRequests.Any()
            ? context.ServiceRequests.Max(r => r.ServiceRequestId)
            : 200;
                request.ServiceRequestId = maxId + 1;
                context.ServiceRequests.Add(request);
                await context.SaveChangesAsync();
                return request;
            }
        }

        public async Task<IEnumerable<ServiceRequest>> GetAllRequestsAsync()
        {
            using (var context = new DatabaseContext())
            {
                return await context.ServiceRequests.ToListAsync();
            }
        }

        public async Task<IEnumerable<ServiceRequest>> GetRequestsByEmployeeIdAsync(int employeeId)
        {
            using (var context = new DatabaseContext())
            {
                return await context.ServiceRequests
                    .Where(r => r.EmployeeId == employeeId)
                    .ToListAsync();
            }
        }

        public async Task<ServiceRequest> GetRequestByIdAsync(int id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.ServiceRequests.FindAsync(id);
            }
        }

        public async Task<ServiceRequest> UpdateRequestStatusAsync(int id, string newStatus)
        {
            using (var context = new DatabaseContext())
            {
                var request = await context.ServiceRequests.FindAsync(id);
                if (request == null) return null;

                request.Status = newStatus;
                await context.SaveChangesAsync();
                return request;
            }
        }

        public async Task<bool> DeleteRequestAsync(int id)
        {
            using (var context = new DatabaseContext())
            {
                var request = await context.ServiceRequests.FindAsync(id);
                if (request == null) return false;

                context.ServiceRequests.Remove(request);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}