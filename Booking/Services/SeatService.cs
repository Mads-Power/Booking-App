using System;
using Booking.Context;
using Booking.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services
{
    public class SeatService
    {
        private readonly OfficeDbContext _context;

        public SeatService(OfficeDbContext context)
        {
            _context = context;
        }
    }
}

