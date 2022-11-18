using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Models.Domain
{
	[Table("User")]
	public class User
	{
		// Primary key
		public int Id { get; set; }
		[Required]
		[MaxLength(255)]
		public string Name { get; set; }
		public bool IsSignedIn { get; set; }

		// Foreign Keys
		public int OfficeId { get; set; }

		public Office Office { get; set; }
		public Seat? Seat { get; set; }

	}
}

