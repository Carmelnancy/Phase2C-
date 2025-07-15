using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL.DataAccess
{
    public class AssetCategoryRepo : IAssetCategoryRepo
    {
        public async Task<AssetCategory> AddAsync(AssetCategory category)
        {
            using (var context = new DatabaseContext())
            {
                context.AssetCategories.Add(category);
                await context.SaveChangesAsync();
                return category;
            }
        }

        public async Task<AssetCategory> UpdateAsync(AssetCategory category)
        {
            using (var context = new DatabaseContext())
            {
                context.AssetCategories.Update(category);
                await context.SaveChangesAsync();
                return category;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var context = new DatabaseContext())
            {
                var category = await context.AssetCategories.FindAsync(id);
                if (category == null) return false;

                context.AssetCategories.Remove(category);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<AssetCategory>> GetAllAsync()
        {
            using (var context = new DatabaseContext())
            {
                return await context.AssetCategories.ToListAsync();
            }
        }

        public async Task<AssetCategory> GetByIdAsync(int id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.AssetCategories.FindAsync(id);
            }
        }
    }

}
