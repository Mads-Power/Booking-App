using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.Models.Domain;
using Booking.Models.DTOs;
using AutoMapper;

namespace Booking.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
            CreateMap<User, UserReadDTO>();
			CreateMap<UserCreateDTO, User>();
			CreateMap<UserEditDTO, User>();
		}
	}
}

