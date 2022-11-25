using System;
using System.Diagnostics.CodeAnalysis;
using BookingApp.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Context
{
	public class OfficeDbContext : DbContext
	{
		public OfficeDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Office> Offices { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create one-to-many Office and User relation by adding foreign key to Office
            modelBuilder.Entity<User>()
                .HasOne<Office>(user => user.Office)
                .WithMany(office => office.Users)
                .HasForeignKey(user => user.OfficeId);

            // Create one-to-many Office and Room relation by adding foreign key to Office
            modelBuilder.Entity<Room>()
                .HasOne<Office>(room => room.Office)
                .WithMany(office => office.Rooms)
                .HasForeignKey(room => room.OfficeId);

            // Create one-to-many Room and Seat relation by adding foreign key to Room
            modelBuilder.Entity<Seat>()
                .HasOne<Room>(seat => seat.Room)
                .WithMany(room => room.Seats)
                .HasForeignKey(seat => seat.RoomId);

            // Create zero/one-to-zero/one User and Seat relation
            var seatUserEtb = modelBuilder.Entity<Booking>();
            seatUserEtb.HasKey(su => new { su.SeatId, su.UserId });
            seatUserEtb.HasIndex(su => su.SeatId).IsUnique();
            seatUserEtb.HasIndex(su => su.UserId).IsUnique();

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

            // Add Rooms
            modelBuilder.Entity<Room>().HasData(new Room
            {
                Id = 1,
                Name = "Storerommet",
                Capacity = 10,
                OfficeId = 1
            });

            modelBuilder.Entity<Room>().HasData(new Room
            {
                Id = 2,
                Name = "Lillerommet",
                Capacity = 5,
                OfficeId = 1
            });

            // Add Seats
            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 1,
                Name = "01",
                IsOccupied = true,
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 2,
                Name = "02",
                IsOccupied = false,
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 3,
                Name = "03",
                IsOccupied = false,
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 4,
                Name = "04",
                IsOccupied = false,
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 5,
                Name = "05",
                IsOccupied = false,
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 6,
                Name = "06",
                IsOccupied = false,
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 7,
                Name = "07",
                IsOccupied = false,
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 8,
                Name = "01",
                IsOccupied = false,
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 9,
                Name = "01",
                IsOccupied = false,
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 10,
                Name = "10",
                IsOccupied = false,
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 11,
                Name = "11",
                IsOccupied = false,
                RoomId = 2
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 12,
                Name = "12",
                IsOccupied = false,
                RoomId = 2
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 13,
                Name = "13",
                IsOccupied = false,
                RoomId = 2
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 14,
                Name = "14",
                IsOccupied = false,
                RoomId = 2
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 15,
                Name = "15",
                IsOccupied = false,
                RoomId = 2
            });

            // Add Users
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Ted Mosby",
                IsSignedIn = true,
                OfficeId = 1
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Name = "Marshall Eriksen",
                IsSignedIn = false,
                OfficeId = 1
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 3,
                Name = "Lily Aldrin",
                IsSignedIn = false,
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

            // Add SeatUsers
            modelBuilder.Entity<Booking>().HasData(new Booking
            {
                SeatId = 1,
                UserId = 1
            });
        }
    }
}

