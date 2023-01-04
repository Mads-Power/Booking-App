using System;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Repositories
{
	public interface IBookingRepository
	{
        public bool BookingExists(int bookingId);
        public Task<List<Booking>> GetBookingsAsync();
        public Task<Booking?> GetBookingAsync(int bookingId);
        public Booking? GetBookingByDateAndUser(DateTime date, int userId);
        public Booking? GetBookingByDateAndSeat(DateTime date, int seatId);
        public Task<Booking> AddAsync(Booking newBooking);
        public Task UpdateAsync(Booking updatedBooking);
        public Task DeleteAsync(int bookingId);
        public Task<Booking> BookSeat(User user, Seat seat, DateTime date);
        public Task UnbookSeat(User user, DateTime date);
    }
}

