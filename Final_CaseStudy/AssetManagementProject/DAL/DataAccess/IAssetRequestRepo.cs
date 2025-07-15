using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
    public interface IAssetRequestRepo
    {
        Task<AssetRequest> AddAssetRequestAsync(AssetRequest request);
        Task<IEnumerable<AssetRequest>> GetAllRequestsAsync();
        Task<IEnumerable<AssetRequest>> GetRequestsByEmployeeIdAsync(int employeeId);
        Task<AssetRequest> GetRequestByIdAsync(int id);
        Task<AssetRequest> UpdateRequestStatusAsync(int id, string newStatus);
        Task<bool> DeleteRequestAsync(int id);
        Task<string> ReturnAssetAsync(int requestId);
    }
}
