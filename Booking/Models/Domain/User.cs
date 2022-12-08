using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models.Domain
{
	[Table("User")]
	public class User
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		public string Name { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }

		public List<Booking> Bookings { get; set; }
	}
}

