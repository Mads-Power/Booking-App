using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Models.Domain
{
	[Table("SeatUser")]
	public class SeatUser
	{
		public int SeatId { get; set; }
		public int UserId { get; set; }
		public virtual Seat Seat { get; set; }
		public virtual User User { get; set; }
	}
}

