using System;
using BookingApp.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models.DTOs
{
    public class BookingBookDTO
    {
        public int SeatId { get; set; }
        public string Email { get; set; }
        public string Date { get; set; } = "";
    }

    public class BookingUnbookDTO
    {
        public string Email { get; set; }
        public string Date { get; set; } = "";
    }
}

