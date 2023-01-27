using System;
using System.Threading.Tasks;
using BookingApp.Context;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly OfficeDbContext _context;

        public UserRepository(OfficeDbContext context)
        {
            _context = context;
        }

        public bool UserExists(string userId)
        {
            return _context.Users.Find(userId) != null;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task<User?> GetUserAsync(string userId)
        {
            return await _context.Users.Include(user => user.Bookings).Where(user => user.Id == userId).FirstOrDefaultAsync();
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

        public async Task DeleteAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // get booked seat for the user
    }
}

