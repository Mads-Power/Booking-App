using System;
using BookingApp.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models.DTOs
{
	public class OfficeEditDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public ICollection<User> Users { get; set; }
    }
}

