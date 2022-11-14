using System;
using System.Diagnostics.CodeAnalysis;
using Booking.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess
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
        }
    }
}

