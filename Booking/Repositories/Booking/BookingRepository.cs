﻿using System;
using BookingApp.Context;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
	public class BookingRepository : IBookingRepository
	{
        private readonly OfficeDbContext _context;

        public BookingRepository(OfficeDbContext context)
        {
            _context = context;
        }

        public bool BookingExists(int bookingId)
        {
            return _context.Bookings.Find(bookingId) != null;
        }

        public async Task<List<Booking>> GetBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetBookingAsync(int bookingId)
        {
            return await _context.Bookings.FindAsync(bookingId);
        }

        public Booking? GetBookingByDateAndUser(DateTime date, string userEmail)
        {
            return _context.Bookings
                .Where(booking => booking.Email == userEmail && DateTime
                .Compare(booking.Date.Date, date.Date) == 0).FirstOrDefault();
        }

        public Booking? GetBookingByDateAndSeat(DateTime date, int seatId)
        {
            return _context.Bookings
                .Where(booking => booking.SeatId == seatId && DateTime
                .Compare(booking.Date.Date, date.Date) == 0).FirstOrDefault();
        }

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

        public async Task<Booking> BookSeat(User user, Seat seat, DateTime date)
        {
            var booking = new Booking { Seat = seat, User = user, Date = date, Email = user.Email };
            _context.Add(booking);

            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task UnbookSeat(User user, DateTime date)
        {
            var booking = GetBookingByDateAndUser(date, user.Email);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);

                await _context.SaveChangesAsync();
            }
        }
    }
}