using System;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Repositories
{
	public interface IOfficeRepository
	{
        public bool OfficeExists(int officeId);
        public Task<List<Office>> GetAllOffices();
        public Task<Office?> GetOfficeAsync(int officeId);
        public Task<Office> AddAsync(Office newOffice);
        public Task UpdateAsync(Office updatedOffice);
        public Task DeleteAsync(int officeId);
    }
}

