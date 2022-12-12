using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models.Domain
{
    [Table("Seat")]
	public class Seat
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        // Foreign Keys
        [Required]
        public int RoomId { get; set; }

        public Room Room { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}

