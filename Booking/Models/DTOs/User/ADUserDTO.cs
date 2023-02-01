using BookingApp.Models.Domain;

namespace BookingApp.Models.DTOs
{
    public class ADUserDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

        public List<BookingReadDTO> Bookings {get; set; }
    }
}