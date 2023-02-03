using System;
using BookingApp.Context;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly OfficeDbContext _context;

        public RoomRepository(OfficeDbContext context)
        {
            _context = context;
        }

        public bool RoomExists(int roomId)
        {
            return _context.Rooms.Find(roomId) != null;
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            return await _context.Rooms.Include(room => room.Seats).ToListAsync();
        }

        public async Task<Room?> GetRoomAsync(int roomId)
        {
            return await _context.Rooms.Include(room => room.Seats)
                .Where(room => room.Id == roomId).FirstAsync();
        }

        public async Task<Room> AddAsync(Room newRoom)
        {
            _context.Rooms.Add(newRoom);
            await _context.SaveChangesAsync();
            return newRoom;
        }

        public async Task UpdateAsync(Room updatedRoom)
        {
            _context.Entry(updatedRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Seat>> GetSeatsInRoom(int roomId)
        {
            return await _context.Seats
                .Where(seat => seat.RoomId == roomId)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsInRoomByDate(int roomId, DateTime date)
        {
            var bookings = (from room in _context.Rooms
                            join seat in _context.Seats
                            on room.Id equals seat.RoomId
                            join booking in _context.Bookings
                            on seat.Id equals booking.SeatId
                            join user in _context.Users
                            on booking.Email equals user.Email
                            where room.Id == roomId where
                            booking.Date.Date == date.Date
                            select booking).ToListAsync();

            return await bookings;
        }
    }
}

