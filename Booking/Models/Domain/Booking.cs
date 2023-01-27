using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models.Domain
{
	[Table("Booking")]
	public class Booking
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public DateTime Date { get; set; }

		// Foreign Keys
		public int SeatId { get; set; }
		public string UserId { get; set; }

		public Seat Seat { get; set; }
		public User User { get; set; }
	}
}

