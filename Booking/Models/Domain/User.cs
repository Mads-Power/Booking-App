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
		// Primary key
		public int Id { get; set; }
		[Required]
		[MaxLength(255)]
		public string Name { get; set; }
		public bool IsSignedIn { get; set; }

		// Foreign Key
		public int OfficeId { get; set; }

		//public List<FavoriteSeat> FavoriteSeats { get; set; }

		public Office Office { get; set; }
		public Booking SeatUser { get; set; }

	}

	//public struct FavoriteSeat
	//{
	//	public Seat Seat { get; set; }
	//	public int TimesBooked { get; set; }
	//}
}

