using DAL.DataAccess;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementTests
{
    [TestFixture]
    public class EmployeeRepoTests
    {
        private DbContextOptions<DatabaseContext> _dbOptions;

        [SetUp]
        public void Setup()
        {
            _dbOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeTestDb")
                .Options;

            // Seed test data
            using (var context = new DatabaseContext(_dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Employees.AddRange(new List<Employee>
                {
                    new Employee
                    {
                        EmployeeId = 1,
                        Name = "John",
                        Email = "john@example.com",
                        Password = "pass123",
                        ContactNumber = "9876543210",
                        Address = "City A",
                        Gender = "Male",
                        Role = "Employee"
                    },
                    new Employee
                    {
                        EmployeeId = 2,
                        Name = "Jane",
                        Email = "jane@example.com",
                        Password = "pass456",
                        ContactNumber = "9999999999",
                        Address = "City B",
                        Gender = "Female",
                        Role = "Employee"
                    }
                });

                context.SaveChanges();
            }
        }

        [Test]
        public async Task AddEmployeeAsync_ShouldAddNewEmployee()
        {
            using var context = new DatabaseContext(_dbOptions);
            var repo = new EmployeeRepo(context);

            var newEmp = new Employee
            {
                EmployeeId = 3,
                Name = "Alice",
                Email = "alice@example.com",
                Password = "alice123",
                ContactNumber = "8888888888",
                Address = "City C",
                Gender = "Female",
                Role = "Employee"
            };

            var result = await repo.AddEmployeeAsync(newEmp);

            Assert.IsNotNull(result);
            Assert.AreEqual("Alice", result.Name);
        }

        [Test]
        public async Task GetAllEmployeesAsync_ShouldReturnAllEmployees()
        {
            using var context = new DatabaseContext(_dbOptions);
            var repo = new EmployeeRepo(context);

            var result = await repo.GetAllEmployeesAsync();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetEmployeeByIdAsync_ShouldReturnCorrectEmployee()
        {
            using var context = new DatabaseContext(_dbOptions);
            var repo = new EmployeeRepo(context);

            var result = await repo.GetEmployeeByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("John", result.Name);
        }

        [Test]
        public async Task GetEmployeeByEmailAsync_ShouldReturnCorrectEmployee()
        {
            using var context = new DatabaseContext(_dbOptions);
            var repo = new EmployeeRepo(context);

            var result = await repo.GetEmployeeByEmailAsync("jane@example.com");

            Assert.IsNotNull(result);
            Assert.AreEqual("Jane", result.Name);
        }

        [Test]
        public async Task ValidateEmployeeAsync_ShouldReturnEmployee_IfCredentialsMatch()
        {
            using var context = new DatabaseContext(_dbOptions);
            var repo = new EmployeeRepo(context);

            var login = new LoginRequest
            {
                Email = "john@example.com",
                Password = "pass123"
            };

            var result = await repo.ValidateEmployeeAsync(login);

            Assert.IsNotNull(result);
            Assert.AreEqual("John", result.Name);
        }

        [Test]
        public async Task UpdateEmployeeAsync_ShouldUpdateEmployeeDetails()
        {
            using var context = new DatabaseContext(_dbOptions);
            var repo = new EmployeeRepo(context);

            var empToUpdate = await repo.GetEmployeeByIdAsync(2);
            empToUpdate.Address = "New City";

            var updated = await repo.UpdateEmployeeAsync(empToUpdate);

            Assert.IsNotNull(updated);
            Assert.AreEqual("New City", updated.Address);
        }

        [Test]
        public async Task DeleteEmployeeAsync_ShouldRemoveEmployee()
        {
            using var context = new DatabaseContext(_dbOptions);
            var repo = new EmployeeRepo(context);

            var deleted = await repo.DeleteEmployeeAsync(1);
            var shouldBeNull = await repo.GetEmployeeByIdAsync(1);

            Assert.IsTrue(deleted);
            Assert.IsNull(shouldBeNull);
        }
    }
}
