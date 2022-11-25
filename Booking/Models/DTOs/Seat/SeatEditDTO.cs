using System;
namespace BookingApp.Models.DTOs
{
	public class SeatEditDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOccupied { get; set; }
        public int RoomId { get; set; }
    }
}