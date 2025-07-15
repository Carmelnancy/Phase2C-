using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
    public interface IAssetRepo
    {
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<Asset> GetAssetByIdAsync(int id);
        Task<Asset> AddAssetAsync(Asset asset);
        Task<Asset> UpdateAssetAsync(Asset asset);
        Task<bool> DeleteAssetAsync(int id);
        Task<IEnumerable<Asset>> GetAssetsByCategoryAsync(int categoryId);
        Task<IEnumerable<Asset>> GetAssetsByStatusAsync(string status);
        Task<IEnumerable<Asset>> GetAssetsByEmployeeIdAsync(int employeeId);

    }
}
