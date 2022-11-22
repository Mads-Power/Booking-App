﻿using System;
using Booking.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Booking.Models.DTOs
{
	public class OfficeReadDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public ICollection<UserReadDTO> Users { get; set; }
        public ICollection<RoomReadDTO> Rooms { get; set; }
    }
}

