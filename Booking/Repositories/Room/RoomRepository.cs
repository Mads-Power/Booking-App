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

        public async Task<List<Room>> GetAllRooms()
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

        public async Task<List<User>> GetSignedInUsersInRoom(int roomId)
        {
            var users = (from room in _context.Rooms
                         join seat in _context.Seats
                         on room.Id equals seat.RoomId
                         join su in _context.Bookings
                         on seat.Id equals su.SeatId
                         join user in _context.Users
                         on su.UserId equals user.Id
                         where room.Id == roomId
                         select user).ToListAsync();

            return await users;
        }
    }
}

