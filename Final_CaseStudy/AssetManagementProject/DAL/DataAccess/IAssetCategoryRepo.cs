using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
    public interface IAssetCategoryRepo
    {
        Task<IEnumerable<AssetCategory>> GetAllAsync();
        Task<AssetCategory> AddAsync(AssetCategory category);
        Task<AssetCategory> UpdateAsync(AssetCategory category);
        Task<bool> DeleteAsync(int id);
        Task<AssetCategory> GetByIdAsync(int id);

    }
}
