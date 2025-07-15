//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DAL.Models;
//using Microsoft.EntityFrameworkCore;

//namespace DAL.DataAccess
//{
//    public class EmployeeRepo : IEmployeeRepo
//    {

//        private readonly DatabaseContext _context;

//        // This constructor allows unit tests to inject InMemory context
//        public EmployeeRepo(DatabaseContext context)
//        {
//            _context = context;
//        }
//        public async Task<Employee> AddEmployeeAsync(Employee employee)
//        {
//            using (var context = new DatabaseContext())
//            {
//                context.Employees.Add(employee);
//                await context.SaveChangesAsync();
//                return employee;
//            }
//        }

//        public async Task<Employee> GetEmployeeByEmailAsync(string email)
//        {
//            using (var context = new DatabaseContext())
//            {
//                return await context.Employees.FirstOrDefaultAsync(e => e.Email == email);
//            }
//        }

//        public async Task<Employee> ValidateEmployeeAsync(LoginRequest login)
//        {
//            using (var context = new DatabaseContext())
//            {
//                return await context.Employees.FirstOrDefaultAsync(e =>
//                    e.Email == login.Email && e.Password == login.Password);
//            }
//        }

//        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
//        {
//            using (var context = new DatabaseContext())
//            {
//                return await context.Employees.ToListAsync();
//            }
//        }

//        public async Task<Employee> GetEmployeeByIdAsync(int id)
//        {
//            using (var context = new DatabaseContext())
//            {
//                return await context.Employees.FindAsync(id);
//            }
//        }

//        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
//        {
//            using (var context = new DatabaseContext())
//            {
//                var existing = await context.Employees.FindAsync(employee.EmployeeId);
//                if (existing == null) return null;

//                existing.Name = employee.Name;
//                existing.Email = employee.Email;
//                existing.Password = employee.Password;
//                existing.ContactNumber = employee.ContactNumber;
//                existing.Address = employee.Address;
//                existing.Gender = employee.Gender;
//                existing.Role = employee.Role;

//                await context.SaveChangesAsync();
//                return existing;
//            }
//        }

//        public async Task<bool> DeleteEmployeeAsync(int id)
//        {
//            using (var context = new DatabaseContext())
//            {
//                var employee = await context.Employees.FindAsync(id);
//                if (employee == null) return false;

//                context.Employees.Remove(employee);
//                await context.SaveChangesAsync();
//                return true;
//            }
//        }

//        public async Task<string> ForgotPasswordAsync(string email)
//        {
//            using (var context = new DatabaseContext())
//            {
//                var employee = await context.Employees.FirstOrDefaultAsync(e => e.Email == email);
//                return employee?.Password;
//            }
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataAccess
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DatabaseContext _context;

        // Constructor-based context for dependency injection (for testing & real use)
        public EmployeeRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Employee> ValidateEmployeeAsync(LoginRequest login)
        {
            return await _context.Employees.FirstOrDefaultAsync(e =>
                e.Email == login.Email && e.Password == login.Password);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var existing = await _context.Employees.FindAsync(employee.EmployeeId);
            if (existing == null) return null;

            existing.Name = employee.Name;
            existing.Email = employee.Email;
            existing.Password = employee.Password;
            existing.ContactNumber = employee.ContactNumber;
            existing.Address = employee.Address;
            existing.Gender = employee.Gender;
            existing.Role = employee.Role;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            return employee?.Password;
        }
    }
}
