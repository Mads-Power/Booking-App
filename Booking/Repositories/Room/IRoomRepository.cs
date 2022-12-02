using System;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Repositories
{
	public interface IRoomRepository
	{
        public bool RoomExists(int roomId);
        public Task<List<Room>> GetRoomsAsync();
        public Task<Room?> GetRoomAsync(int roomId);
        public Task<Room> AddAsync(Room newRoom);
        public Task UpdateAsync(Room updatedRoom);
        public Task DeleteAsync(int roomId);
        public Task<List<Seat>> GetSeatsInRoom(int roomId);
        public Task<List<Booking>> GetTodaysBookingsInRoom(int roomId, DateTime date);
    }
}

