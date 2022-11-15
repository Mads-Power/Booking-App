using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.Models.Domain;
using Booking.Models.DTOs;
using AutoMapper;

namespace Booking.Profiles
{
	public class OfficeProfile : Profile
	{
		public OfficeProfile()
		{
			CreateMap<Office, OfficeReadDTO>();
            CreateMap<OfficeCreateDTO, Office>()
				.ForMember(o => o.Users, opt => opt
				.Ignore());
            CreateMap<OfficeEditDTO, Office>()
				.ForMember(o => o.Users, opt => opt
				.Ignore());
        }
	}
}

