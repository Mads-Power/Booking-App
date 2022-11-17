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

        public async Task<List<User>> GetAllUsersFromOffice()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task<User> GetUserAsync(int userId)
        {
            var usersInOffice = await GetAllUsersFromOffice();
            var user = usersInOffice.FirstOrDefault(u => u.Id == userId);
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SignIn(int userId)
        {
            var user = GetUserAsync(userId).Result;
            user.IsSignedIn = true;
            await UpdateAsync(user);
        }

        public async Task SignOut(int userId)
        {
            var user = GetUserAsync(userId).Result;
            user.IsSignedIn = false;
            await UpdateAsync(user);
        }
    }
}

