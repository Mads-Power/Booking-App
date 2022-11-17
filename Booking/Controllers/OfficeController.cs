using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Booking.Context;
using Booking.Models.Domain;
using Booking.Models.DTOs;
using Booking.Services;

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly OfficeService _officeService;
        private readonly UserService _userService;

        public OfficeController(IMapper mapper, OfficeService officeService, UserService userService)
        {
            _mapper = mapper;
            _officeService = officeService;
            _userService = userService;
        }

        // HTTP requests
    }
}

