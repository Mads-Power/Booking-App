using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models.Domain;
using BookingApp.Models.DTOs;
using AutoMapper;

namespace BookingApp.Profiles
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

