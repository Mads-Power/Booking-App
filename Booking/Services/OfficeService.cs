using System;
using BookingApp.Context;
using BookingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Services
{
	public class OfficeService
	{
		private readonly OfficeDbContext _context;

		public OfficeService(OfficeDbContext context)
		{
			_context = context;
		}

        public bool OfficeExists(int officeId)
        {
            return _context.Offices.Find(officeId) != null;
        }

        public async Task<List<Office>> GetAllOffices()
        {
            return await _context.Offices.Include(office => office.Rooms)
                .Include(office => office.Users).ToListAsync();
        }

        public async Task<Office?> GetOfficeAsync(int officeId)
        {
            return await _context.Offices.Include(office => office.Rooms)
                .Include(office => office.Users)
                .Where(office => office.Id == officeId).FirstAsync();
        }

        public async Task<Office> AddAsync(Office newOffice)
        {
            _context.Offices.Add(newOffice);
            await _context.SaveChangesAsync();
            return newOffice;
        }

        public async Task UpdateAsync(Office updatedOffice)
        {
            _context.Entry(updatedOffice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int officeId)
        {
            var office = await _context.Offices.FindAsync(officeId);
            if (office != null)
            {
                _context.Offices.Remove(office);
                await _context.SaveChangesAsync();
            }
        }
    }
}

