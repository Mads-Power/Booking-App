using System;
using BookingApp.Models.Domain;

namespace BookingApp.Models.DTOs
{
	public class UserReadDTO
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<BookingReadDTO> Bookings { get; set; }
    }
}