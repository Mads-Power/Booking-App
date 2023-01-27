using System;
using System.Threading.Tasks;
using BookingApp.Context;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace BookingApp.Repositories

{
	public interface IUserRepository
	{
		public bool UserExists(string userEmail);
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User?> GetUserAsync(string userEmail);
        public Task<User> AddAsync(User newUser);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(string userEmail);
    }
}

