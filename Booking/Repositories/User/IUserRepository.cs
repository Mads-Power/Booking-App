using System;
using System.Threading.Tasks;
using BookingApp.Context;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace BookingApp.Repositories

{
	public interface IUserRepository
	{
		public bool UserExists(string userId);
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User?> GetUserAsync(string userId);
        public Task<User> AddAsync(User newUser);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(string userId);
    }
}

