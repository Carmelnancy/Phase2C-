using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
    public interface IAdminRepo
    {
        Task<Admin> AddAdminAsync(Admin admin);
        Task<Admin> GetAdminByEmailAsync(string email);
        Task<Admin> ValidateAdminAsync(LoginRequest login);
        Task<IEnumerable<Admin>> GetAllAdminsAsync();
        Task<Admin> GetAdminByIdAsync(int id);
        Task<Admin> UpdateAdminAsync(Admin admin);
        Task<bool> DeleteAdminAsync(int id);
        Task<string> ForgotPasswordAsync(string email);
    }
}
