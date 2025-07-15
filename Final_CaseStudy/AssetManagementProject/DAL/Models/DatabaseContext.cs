using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AssetCategory> AssetCategories { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetRequest> AssetRequests { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<AuditRequest> AuditRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //To configure a connection string
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DatabaseHelper.GetConnectionString());
            }

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique Constraints
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Asset>()
                .HasIndex(a => a.AssetNo)
                .IsUnique();

            // Relationships (no navigation properties used)
            modelBuilder.Entity<Asset>()
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);  // If Employee deleted, Asset stays (nullified)

            modelBuilder.Entity<Asset>()
                .HasOne<AssetCategory>()
                .WithMany()
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Don't allow delete if Assets exist in Category

            modelBuilder.Entity<AssetRequest>()
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // If employee is deleted, delete their requests

            modelBuilder.Entity<AssetRequest>()
                .HasOne<Asset>()
                .WithMany()
                .HasForeignKey(r => r.AssetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceRequest>()
                .HasOne<Asset>()
                .WithMany()
                .HasForeignKey(r => r.AssetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuditRequest>()
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed default Admin data
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = 1,
                    Name = "Super Admin",
                    Email = "admin@asset.com",
                    Password = "admin123",
                    Role="Admin",
                    Gender = "Male",
                    ContactNumber = "9876543210",
                    Address = "Head Office"
                }
            );
        }
    }
}
