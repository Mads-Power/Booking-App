using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models.Domain;
using BookingApp.Models.DTOs;
using AutoMapper;

namespace BookingApp.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomReadDTO>();
            CreateMap<RoomCreateDTO, Room>();
            CreateMap<RoomEditDTO, Room>();
        }
    }
}

