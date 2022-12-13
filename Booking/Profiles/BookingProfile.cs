using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models.Domain;
using BookingApp.Models.DTOs;
using AutoMapper;

namespace BookingApp.Profiles
{
	public class BookingProfile : Profile
	{
		public BookingProfile()
		{
			CreateMap<Booking, BookingReadDTO>();
			CreateMap<BookingCreateDTO, Booking>().ForMember(b => b.Date, opt => opt.Ignore());
			CreateMap<BookingEditDTO, Booking>();
			CreateMap<BookingBookDTO, Booking>().ForMember(b => b.Id, opt => opt.Ignore()).ForMember(b => b.Date, opt => opt.Ignore());
        }
	}
}

