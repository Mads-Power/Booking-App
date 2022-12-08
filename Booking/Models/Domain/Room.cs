using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models.Domain
{
	[Table("Room")]
	public class Room
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Capacity { get; set; }

        // Foreign Key
        public int OfficeId { get; set; }

        public List<Seat> Seats { get; set; }
        public Office Office { get; set; }
    }
}

