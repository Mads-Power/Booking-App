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

		public int SeatId { get; set; }
		public int UserId { get; set; }
		public virtual Seat Seat { get; set; }
		public virtual User User { get; set; }
	}
}

