using System;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models.DTOs
{
	public class UserCreateDTO
	{
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}