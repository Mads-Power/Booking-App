using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models.Domain;
using BookingApp.Models.DTOs;
using AutoMapper;

namespace BookingApp.Profiles
{
	public class OfficeProfile : Profile
	{
		public OfficeProfile()
		{
			CreateMap<Office, OfficeReadDTO>();
			CreateMap<OfficeCreateDTO, Office>();
			CreateMap<OfficeEditDTO, Office>();
        }
	}
}

