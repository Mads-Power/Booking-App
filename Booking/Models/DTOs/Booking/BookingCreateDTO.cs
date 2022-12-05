using System;
using BookingApp.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models.DTOs
{
	public class BookingCreateDTO
	{
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}

