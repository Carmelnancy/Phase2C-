using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataAccess
{
    public class AssetRequestRepo : IAssetRequestRepo
    {
        public async Task<AssetRequest> AddAssetRequestAsync(AssetRequest request)
        {
            using (var context = new DatabaseContext())
            {
                int maxId = context.AssetRequests.Any()
                ? context.AssetRequests.Max(r => r.RequestId): 100; // or 0, your choice

                request.RequestId = maxId + 1;
                context.AssetRequests.Add(request);
                await context.SaveChangesAsync();
                return request;
            }
        }

        public async Task<IEnumerable<AssetRequest>> GetAllRequestsAsync()
        {
            using (var context = new DatabaseContext())
            {
                return await context.AssetRequests.ToListAsync();
            }
        }

        public async Task<IEnumerable<AssetRequest>> GetRequestsByEmployeeIdAsync(int employeeId)
        {
            using (var context = new DatabaseContext())
            {
                return await context.AssetRequests
                    .Where(r => r.EmployeeId == employeeId)
                    .ToListAsync();
            }
        }

        public async Task<AssetRequest> GetRequestByIdAsync(int id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.AssetRequests.FindAsync(id);
            }
        }

        public async Task<AssetRequest> UpdateRequestStatusAsync(int id, string newStatus)
        {
            using (var context = new DatabaseContext())
            {
                var request = await context.AssetRequests.FindAsync(id);
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
                var request = await context.AssetRequests.FindAsync(id);
                if (request == null) return false;

                context.AssetRequests.Remove(request);
                await context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<string> ReturnAssetAsync(int requestId)
        {
            using (var context = new DatabaseContext())
            {
                var request = await context.AssetRequests.FindAsync(requestId);
                if (request == null) return "Request not found.";

                if (request.Status != "Approved")
                    return "Only approved requests can be returned.";

                var asset = await context.Assets.FindAsync(request.AssetId);
                if (asset == null) return "Asset not found.";

                asset.Quantity += 1;
                request.Status = "Returned";

                await context.SaveChangesAsync();
                return $"Asset returned by Employee {request.EmployeeId}.";
            }
        }

    }
}
