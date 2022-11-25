using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models.Domain
{
	[Table("Office")]
	public class Office
	{
		// Primary key
		public int Id { get; set; }

		[Required]
		[MaxLength(255)]
		public string Name { get; set; }
		public int Capacity { get; set; }

		public ICollection<User> Users { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}

