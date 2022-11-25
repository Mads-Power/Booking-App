using System;
using BookingApp.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models.DTOs
{
	public class RoomEditDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int OfficeId { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}

