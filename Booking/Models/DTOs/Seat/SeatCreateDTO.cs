using System;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models.DTOs
{
	public class SeatCreateDTO
	{
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int RoomId { get; set; }
    }
}