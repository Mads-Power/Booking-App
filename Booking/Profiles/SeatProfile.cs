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
            CreateMap<SeatCreateDTO, Seat>()
                .ForMember(u => u.RoomId, opt => opt
                .Ignore());
            CreateMap<SeatEditDTO, Seat>()
                .ForMember(u => u.RoomId, opt => opt
                .Ignore());
        }
    }
}

