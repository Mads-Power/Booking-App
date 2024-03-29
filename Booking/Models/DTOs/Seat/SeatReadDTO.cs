﻿using System;
using BookingApp.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models.DTOs
{
	public class SeatReadDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomId { get; set; }
        public ICollection<BookingReadDTO> Bookings { get; set; }
    }
}