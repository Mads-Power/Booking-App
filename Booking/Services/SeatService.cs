using System;
using Booking.Context;
using Booking.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services
{
    public class SeatService
    {
        private readonly OfficeDbContext _context;

        public SeatService(OfficeDbContext context)
        {
            _context = context;
        }

        public bool SeatExists(int seatId)
        {
            return _context.Seats.Find(seatId) != null;
        }

        public async Task<List<Seat>> GetAllSeats()
        {
            return await _context.Seats.ToListAsync();
        }

        public async Task<Seat?> GetSeatAsync(int seatId)
        {
            return await _context.Seats.FindAsync(seatId);
        }

        public async Task<Seat> AddAsync(Seat newSeat)
        {
            _context.Seats.Add(newSeat);
            await _context.SaveChangesAsync();
            return newSeat;
        }

        public async Task UpdateAsync(Seat updatedSeat)
        {
            _context.Entry(updatedSeat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int seatId)
        {
            var seat = await _context.Seats.FindAsync(seatId);
            if (seat != null)
            {
                _context.Seats.Remove(seat);
                await _context.SaveChangesAsync();
            }
        }
    }
}

