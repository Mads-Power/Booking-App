using System;
using BookingApp.Models.Domain;

namespace BookingApp.Models.DTOs
{
	public class UserReadDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int OfficeId { get; set; }
        public ICollection<BookingReadDTO> Bookings { get; set; }
    }
}