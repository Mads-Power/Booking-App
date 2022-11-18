using System;
namespace Booking.Models.DTOs
{
	public class UserReadDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSignedIn { get; set; }
        public int OfficeId { get; set; }
        public int? SeatId { get; set; }
    }
}