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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            // Create one-to-many User and Booking relation
            modelBuilder.Entity<Booking>()
                .HasOne<User>(booking => booking.User)
                .WithMany(user => user.Bookings)
                .HasForeignKey(booking => booking.UserId);

            // Create one-to-many Seat and Booking relation
            modelBuilder.Entity<Booking>()
                .HasOne<Seat>(booking => booking.Seat)
                .WithMany(seat => seat.Bookings)
                .HasForeignKey(booking => booking.SeatId);

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
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 2,
                Name = "02",
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 3,
                Name = "03",
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 4,
                Name = "04",
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 5,
                Name = "05",
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 6,
                Name = "06",
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 7,
                Name = "07",
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 8,
                Name = "01",
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 9,
                Name = "01",
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 10,
                Name = "10",
                RoomId = 1
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 11,
                Name = "11",
                RoomId = 2
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 12,
                Name = "12",
                RoomId = 2
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 13,
                Name = "13",
                RoomId = 2
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 14,
                Name = "14",
                RoomId = 2
            });

            modelBuilder.Entity<Seat>().HasData(new Seat
            {
                Id = 15,
                Name = "15",
                RoomId = 2
            });

            // Add Users
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Ted Mosby",
                Email = "ted.mosby@himym.com",
                PhoneNumber = "44112233"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Name = "Marshall Eriksen",
                Email = "marshall.eriksen@himym.com",
                PhoneNumber = "44223344"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 3,
                Name = "Lily Aldrin",
                Email = "lily.aldrin@himym.com",
                PhoneNumber = "44334455"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 4,
                Name = "Barney Stinson",
                Email = "barney.stinson@himym.com",
                PhoneNumber = "44445566"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 5,
                Name = "Robin Scherbatsky",
                Email = "robin.scherbatsky@himym.com",
                PhoneNumber = "44556677"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 6,
                Name = "Ross Geller",
                Email = "ross.geller@friends.com",
                PhoneNumber = "99112233"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 7,
                Name = "Monica Geller",
                Email = "monica.geller@friends.com",
                PhoneNumber = "99223344"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 8,
                Name = "Phoebe Buffay",
                Email = "phoebe.buffay@friends.com",
                PhoneNumber = "99334455"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 9,
                Name = "Chandler Bing",
                Email = "chandler.bing@friends.com",
                PhoneNumber = "99445566"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 10,
                Name = "Joey Tribiani",
                Email = "joey.tribiani@friends.com",
                PhoneNumber = "99556677"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 11,
                Name = "Rachel Green",
                Email = "rachel.green@friends.com",
                PhoneNumber = "99667788"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 12,
                Name = "Test User",
                Email = "test.user@testing.com",
                PhoneNumber = "98765432"
            });

            // Add Bookings
            createBookings(modelBuilder);
        }

        private static void createBookings(ModelBuilder modelBuilder)
        {
            for (int i = 1; i <= 11; i++)
            {
                var userId = i;
                var seatId = 0;
                if (i < 6)
                {
                    seatId = 10 + i;
                }
                else
                {
                    seatId = i - 5;
                }
                for (int j = 1; j <= 20; j++)
                {
                    var bookingId = ((i-1)*20) + j ;
                    var dateOffset = j-1;
                    modelBuilder.Entity<Booking>().HasData(new Booking
                    {
                        Id = bookingId,
                        SeatId = seatId,
                        UserId = userId,
                        Date = DateTime.Today.AddDays(dateOffset).ToUniversalTime()
                    }) ;
                }
            }
        }
    }
}

