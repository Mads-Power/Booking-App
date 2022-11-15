using System;
using Booking.Context;

namespace Booking.Services
{
	public class OfficeService
	{
		private readonly OfficeDbContext _context;

		public OfficeService(OfficeDbContext context)
		{
			_context = context;
		}

	}
}

