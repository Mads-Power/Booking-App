﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Booking.Context;
using Booking.Models.Domain;
using Booking.Models.DTOs;
using Booking.Services;
using System.ComponentModel.DataAnnotations;

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly OfficeService _officeService;
        private readonly UserService _userService;

        public UserController(IMapper mapper, OfficeService officeService, UserService userService)
        {
            _mapper = mapper;
            _officeService = officeService;
            _userService = userService;
        }

        // HTTP requests

        /// <summary>
        /// Fetch all users from the database.
        /// </summary>
        /// <returns>A list of User DTOs </returns>
        [HttpGet]
        public async Task<ActionResult<List<UserReadDTO>>> GetAllUsersFromOffice()
        {
            var users = await _userService.GetAllUsersFromOffice();

            return _mapper.Map<List<UserReadDTO>>(users);
        }

        /// <summary>
        /// Fetch a specific user from the database.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserReadDTO>> GetUser(int userId)
        {
            try
            {
                var user = await _userService.GetUserAsync(userId);

                return _mapper.Map<UserReadDTO>(user);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPut("SignIn/{userId}")]
        public async Task<IActionResult> SignInUser(int userId)
        {
            if (_userService.GetUserAsync(userId) == null) return NotFound();

            await _userService.SignIn(userId);
            
            return NoContent();
        }

        [HttpPut("SignOut/{userId}")]
        public async Task<IActionResult> SignOutUser(int userId)
        {
            if (_userService.GetUserAsync(userId) == null) return NotFound();

            await _userService.SignOut(userId);

            return NoContent();
        }

        // validationresult?
    }
}