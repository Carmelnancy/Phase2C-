using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataAccess
{
    public class AuditRequestRepo : IAuditRequestRepo
    {
        public async Task<AuditRequest> AddAuditRequestAsync(AuditRequest request)
        {
            using (var context = new DatabaseContext())
            {
                int maxId = context.AuditRequests.Any()
            ? context.AuditRequests.Max(r => r.AuditRequestId)
            : 300;

                request.AuditRequestId = maxId + 1;

                context.AuditRequests.Add(request);
                await context.SaveChangesAsync();
                return request;
            }
        }

        public async Task<IEnumerable<AuditRequest>> GetAllRequestsAsync()
        {
            using (var context = new DatabaseContext())
            {
                return await context.AuditRequests.ToListAsync();
            }
        }

        public async Task<IEnumerable<AuditRequest>> GetRequestsByEmployeeIdAsync(int employeeId)
        {
            using (var context = new DatabaseContext())
            {
                return await context.AuditRequests
                    .Where(r => r.EmployeeId == employeeId)
                    .ToListAsync();
            }
        }

        public async Task<AuditRequest> GetRequestByIdAsync(int id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.AuditRequests.FindAsync(id);
            }
        }

        public async Task<AuditRequest> UpdateRequestStatusAsync(int id, string newStatus)
        {
            using (var context = new DatabaseContext())
            {
                var request = await context.AuditRequests.FindAsync(id);
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
                var request = await context.AuditRequests.FindAsync(id);
                if (request == null) return false;

                context.AuditRequests.Remove(request);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
