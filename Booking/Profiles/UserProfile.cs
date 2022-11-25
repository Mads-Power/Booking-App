using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models.Domain;
using BookingApp.Models.DTOs;
using AutoMapper;

namespace BookingApp.Profiles
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

