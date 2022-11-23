using System;
using System.Threading.Tasks;
using Booking.Context;
using Booking.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.Services
{
	public class UserService
	{
        private readonly OfficeDbContext _context;

        public UserService(OfficeDbContext context)
        {
            _context = context;
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Find(userId) != null;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task<User?> GetUserAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> AddAsync(User newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task BookSeat(User user, Seat seat)
        {
            _context.Add(new SeatUser { Seat = seat, User = user });
            user.IsSignedIn = true;
            seat.IsOccupied = true;
            _context.Entry(user).State = EntityState.Modified;
            _context.Entry(seat).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
        }

        public async Task UnbookSeat(User user, Seat seat)
        {
            var su = await _context.SeatUsers.FindAsync(seat.Id,user.Id);
            if (su != null)
            {
                _context.SeatUsers.Remove(su);
                user.IsSignedIn = false;
                seat.IsOccupied = false;
                _context.Entry(user).State = EntityState.Modified;
                _context.Entry(seat).State = EntityState.Modified;
                
                await _context.SaveChangesAsync();
            }
        }

        // get booked seat for the user
    }
}

