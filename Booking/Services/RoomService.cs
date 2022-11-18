using System;
using Booking.Context;
using Booking.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services
{
    public class RoomService
    {
        private readonly OfficeDbContext _context;

        public RoomService(OfficeDbContext context)
        {
            _context = context;
        }

        public bool RoomExists(int roomId)
        {
            return _context.Rooms.Find(roomId) != null;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomAsync(int roomId)
        {
            return await _context.Rooms.FindAsync(roomId);
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
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
    }
}

