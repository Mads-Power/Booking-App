using System;
using Booking.Context;
using Booking.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

