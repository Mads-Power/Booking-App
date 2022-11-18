﻿using System;
namespace Booking.Models.DTOs
{
	public class UserCreateDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSignedIn { get; set; }
        public int OfficeId { get; set; }
        public int? SeatId { get; set; }
    }
}