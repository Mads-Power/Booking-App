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
        public int RoomId { get; set; }

        public Room Room { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}

