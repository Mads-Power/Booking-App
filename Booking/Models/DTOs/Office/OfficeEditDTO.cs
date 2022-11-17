﻿using System;
using Booking.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Booking.Models.DTOs
{
	public class OfficeEditDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
