using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataAccess
{
    public class AssetRepo : IAssetRepo
    {
        public async Task<Asset> AddAssetAsync(Asset asset)
        {
            using (var context = new DatabaseContext())
            {
                context.Assets.Add(asset);
                await context.SaveChangesAsync();
                return asset;
            }
        }

        public async Task<Asset> UpdateAssetAsync(Asset asset)
        {
            using (var context = new DatabaseContext())
            {
                context.Assets.Update(asset);
                await context.SaveChangesAsync();
                return asset;
            }
        }

        public async Task<bool> DeleteAssetAsync(int id)
        {
            using (var context = new DatabaseContext())
            {
                var asset = await context.Assets.FindAsync(id);
                if (asset == null) return false;

                context.Assets.Remove(asset);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            using (var context = new DatabaseContext())
            {
                return await context.Assets.ToListAsync();
            }
        }

        public async Task<Asset> GetAssetByIdAsync(int id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Assets.FindAsync(id);
            }
        }

        public async Task<IEnumerable<Asset>> GetAssetsByCategoryAsync(int categoryId)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Assets
                    .Where(a => a.CategoryId == categoryId)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Asset>> GetAssetsByStatusAsync(string status)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Assets
                    .Where(a => a.Status == status)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Asset>> GetAssetsByEmployeeIdAsync(int employeeId)
        {
            //using (var context = new DatabaseContext())
            //{
            //    return await context.Assets
            //        .Where(a => a.EmployeeId == employeeId)
            //        .ToListAsync();
            //}
            using (var context = new DatabaseContext())
            {
                var assets = await (from ar in context.AssetRequests
                                    join a in context.Assets on ar.AssetId equals a.AssetId
                                    where ar.EmployeeId == employeeId && ar.Status == "Approved"
                                    select a).ToListAsync();

                return assets;
            }
        }
    }
}
