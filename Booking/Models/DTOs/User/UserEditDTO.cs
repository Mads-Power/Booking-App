using System;
namespace BookingApp.Models.DTOs
{
	public class UserEditDTO
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}