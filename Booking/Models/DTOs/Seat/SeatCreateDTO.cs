using System;
namespace Booking.Models.DTOs
{
	public class SeatCreateDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOccupied { get; set; }
        public int RoomId { get; set; }
        public int? UserId { get; set; }
    }
}