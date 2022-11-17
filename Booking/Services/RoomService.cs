using System;
using Booking.Context;
using Booking.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services
{
    public class RoomService
    {
        private readonly OfficeDbContext _context;

        public RoomService(OfficeDbContext context)
        {
            _context = context;
        }
    }
}

