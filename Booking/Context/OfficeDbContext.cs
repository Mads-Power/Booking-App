using System;
using System.Diagnostics.CodeAnalysis;
using Booking.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Booking.Context
{
	public class OfficeDbContext : DbContext
	{
		public OfficeDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Office> Offices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create one-to-many Office and User relation by adding foreign key to Office
            modelBuilder.Entity<User>()
                .HasOne<Office>(user => user.Office)
                .WithMany(office => office.Users)
                .HasForeignKey(user => user.OfficeId);

            // Seed dummy data
            AddSeedingData(modelBuilder);
        }

        private static void AddSeedingData(ModelBuilder modelBuilder)
        {
            // Add Office
            modelBuilder.Entity<Office>().HasData(new Office
            {
                Id = 1,
                Name = "Lille Grensen",
                Capacity = 20
            });

            // Add Users
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Ted Mosby",
                IsSignedIn = false,
                OfficeId = 1
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Name = "Marshall Eriksen",
                IsSignedIn = true,
                OfficeId = 1
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 3,
                Name = "Lily Aldrin",
                IsSignedIn = true,
                OfficeId = 1
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 4,
                Name = "Barney Stinson",
                IsSignedIn = false,
                OfficeId = 1
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 5,
                Name = "Robin Scherbatsky",
                IsSignedIn = false,
                OfficeId = 1
            });
        }
    }
}

