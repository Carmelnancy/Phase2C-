using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataAccess
{
    public class AdminRepo : IAdminRepo
    {
        public async Task<Admin> AddAdminAsync(Admin admin)
        {
            using (var context = new DatabaseContext())
            {
                context.Admins.Add(admin);
                await context.SaveChangesAsync();
                return admin;
            }
        }

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Admins.FirstOrDefaultAsync(a => a.Email == email);
            }
        }

        public async Task<Admin> ValidateAdminAsync(LoginRequest login)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Admins.FirstOrDefaultAsync(a =>
                    a.Email == login.Email && a.Password == login.Password);
            }
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
        {
            using (var context = new DatabaseContext())
            {
                return await context.Admins.ToListAsync();
            }
        }

        public async Task<Admin> GetAdminByIdAsync(int id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Admins.FindAsync(id);
            }
        }

        public async Task<Admin> UpdateAdminAsync(Admin admin)
        {
            using (var context = new DatabaseContext())
            {
                var existing = await context.Admins.FindAsync(admin.AdminId);
                if (existing == null) return null;

                existing.Name = admin.Name;
                existing.Email = admin.Email;
                existing.Password = admin.Password;
                existing.ContactNumber = admin.ContactNumber;
                existing.Address = admin.Address;
                existing.Gender = admin.Gender;

                await context.SaveChangesAsync();
                return existing;
            }
        }

        public async Task<bool> DeleteAdminAsync(int id)
        {
            using (var context = new DatabaseContext())
            {
                var admin = await context.Admins.FindAsync(id);
                if (admin == null) return false;

                context.Admins.Remove(admin);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            using (var context = new DatabaseContext())
            {
                var admin = await context.Admins.FirstOrDefaultAsync(a => a.Email == email);
                return admin?.Password;
            }
        }
    }
}