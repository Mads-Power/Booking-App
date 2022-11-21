using System;
using Booking.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Booking.Models.DTOs
{
	public class RoomReadDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int OfficeId { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}