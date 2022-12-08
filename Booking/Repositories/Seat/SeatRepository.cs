using System;
using BookingApp.Context;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly OfficeDbContext _context;

        public SeatRepository(OfficeDbContext context)
        {
            _context = context;
        }

        public bool SeatExists(int seatId)
        {
            return _context.Seats.Find(seatId) != null;
        }

        public async Task<List<Seat>> GetSeatsAsync()
        {
            return await _context.Seats.ToListAsync();
        }

        public async Task<Seat?> GetSeatAsync(int seatId)
        {
            var seat = await _context.Seats.Include(seat => seat.Bookings).Where(seat => seat.Id == seatId).FirstOrDefaultAsync();

            return seat;
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

