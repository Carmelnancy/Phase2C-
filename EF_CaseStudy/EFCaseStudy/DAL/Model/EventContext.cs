using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Model
{
    public class EventContext : DbContext
    {
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<EventDetails> Events { get; set; }
        public DbSet<SpeakersDetails> Speakers { get; set; }
        public DbSet<SessionInfo> Sessions { get; set; }
        public DbSet<ParticipantEventDetails> ParticipantEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<SessionInfo>()
            //    .HasOne<EventDetails>()
            //    .WithMany()
            //    .HasForeignKey(s => s.EventId);
            modelBuilder.Entity<SessionInfo>()
    .HasOne<EventDetails>()
    .WithMany()
    .HasForeignKey(s => s.EventId)
    .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<UserInfo>().HasData(
                new UserInfo
                {
                    EmailId = "admin@gmail.com",
                    UserName = "Admin",
                    Password = "admin123",
                    Role = "Admin"
                });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DatabaseHelper.GetConnectionString());
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
