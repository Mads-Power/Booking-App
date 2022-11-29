using System;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Repositories
{
	public interface ISeatRepository
	{
        public bool SeatExists(int seatId);
        public Task<List<Seat>> GetAllSeats();
        public Task<Seat?> GetSeatAsync(int seatId);
        public Task<Seat> AddAsync(Seat newSeat);
        public Task UpdateAsync(Seat updatedSeat);
        public Task DeleteAsync(int seatId);
    }
}

