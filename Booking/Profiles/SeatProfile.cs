using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.Models.Domain;
using Booking.Models.DTOs;
using AutoMapper;

namespace Booking.Profiles
{
    public class SeatProfile : Profile
    {
        public SeatProfile()
        {
            CreateMap<Seat, SeatReadDTO>();
            CreateMap<SeatCreateDTO, Seat>();
            CreateMap<SeatEditDTO, Seat>();
        }
    }
}

