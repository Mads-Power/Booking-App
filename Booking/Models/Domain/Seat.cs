using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Models.Domain
{
    [Table("Seat")]
	public class Seat
	{
        // Primary key
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public bool IsOccupied { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}

