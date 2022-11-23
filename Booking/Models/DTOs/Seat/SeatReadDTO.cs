using System;
namespace Booking.Models.DTOs
{
	public class SeatReadDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOccupied { get; set; }
        public int RoomId { get; set; }
    }
}