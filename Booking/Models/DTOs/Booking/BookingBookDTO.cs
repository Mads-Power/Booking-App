using System;
using BookingApp.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models.DTOs
{
    public class BookingBookDTO
    {
        public int SeatId { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
    }
}

