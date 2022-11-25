using System;
using BookingApp.Context;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookingApp.Services
{
	public class BookingService
	{
        private readonly OfficeDbContext _context;

        public BookingService(OfficeDbContext context)
        {
            _context = context;
        }

        public bool BookingExists(int bookingId)
        {
            return _context.Bookings.Find(bookingId) != null;
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetBookingAsync(int bookingId)
        {
            return await _context.Bookings.FindAsync(bookingId);
        }

        public Booking? GetBookingByDateAndUserAsync(DateTime date, int userId)
        {
            return _context.Bookings
                .Where(booking => booking.UserId == userId && DateTime
                .Compare(booking.Date, date) == 0).First();
        }

        //public async Task<Booking?> GetBookingByDateAndSeatAsync(DateTime date, int seatId)
        //{

        //}

        public async Task<Booking> AddAsync(Booking newBooking)
        {
            _context.Bookings.Add(newBooking);
            await _context.SaveChangesAsync();
            return newBooking;
        }

        public async Task UpdateAsync(Booking updatedBooking)
        {
            _context.Entry(updatedBooking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task BookSeat(User user, Seat seat, DateTime date)
        {
            _context.Add(new Booking { Seat = seat, User = user, Date = date });

            await _context.SaveChangesAsync();
        }

        public async Task UnbookSeat(User user, Seat seat, DateTime date)
        {
            var booking = GetBookingByDateAndUserAsync(date, user.Id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);

                await _context.SaveChangesAsync();
            }
        }
    }
}