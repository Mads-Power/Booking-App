using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Models.Domain
{
	[Table("Room")]
	public class Room
	{
        // Primary key
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }
        public int Capacity { get; set; }

        public ICollection<Seat> Seats { get; set; }
    }
}

