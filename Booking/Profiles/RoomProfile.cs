using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.Models.Domain;
using Booking.Models.DTOs;
using AutoMapper;

namespace Booking.Profiles
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

