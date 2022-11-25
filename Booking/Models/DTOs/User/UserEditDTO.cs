using System;
namespace BookingApp.Models.DTOs
{
	public class UserEditDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int OfficeId { get; set; }
    }
}