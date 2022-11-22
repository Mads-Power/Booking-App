using System;
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
        private readonly RoomService _roomService;
        private readonly SeatService _seatService;

        public UserController(IMapper mapper, OfficeService officeService, UserService userService, RoomService roomService, SeatService seatService)
        {
            _mapper = mapper;
            _officeService = officeService;
            _userService = userService;
            _roomService = roomService;
            _seatService = seatService;
        }

        // HTTP requests

        /// <summary>
        /// Fetch all users from the database.
        /// </summary>
        /// <returns>A list of User DTOs </returns>
        [HttpGet]
        public async Task<ActionResult<List<UserReadDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

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

        // post: add new user
        [HttpPost]
        public async Task<ActionResult<UserCreateDTO>> PostUser(UserCreateDTO dtoUser)
        {
            // validate request and if not validated return BadRequest()?

            var domainUser = _mapper.Map<User>(dtoUser);

            await _userService.AddAsync(domainUser);

            return CreatedAtAction("GetUser",
                new { userId = domainUser.Id },
                _mapper.Map<UserReadDTO>(domainUser));
        }

        // put: ...
        [HttpPut("userId")]
        public async Task<IActionResult> PutUser(int userId, UserEditDTO userDto)
        {
            if (userId != userDto.Id)
            {
                return BadRequest();
            }

            var domainUser = await _userService.GetUserAsync(userId);

            if (domainUser != null)
            {
                _mapper.Map<UserEditDTO,User>(userDto,domainUser);
            }
            else
            {
                return NotFound();
            }

            try
            {
                await _userService.UpdateAsync(domainUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // delete: delete user
        [HttpDelete("userId")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            await _userService.DeleteAsync(userId);

            return NoContent();
        }

        [HttpPut("SignIn/{userId}")]
        public async Task<IActionResult> SignInUser(int userId)
        {
            if (!_userService.UserExists(userId)) return NotFound();

            await _userService.SignIn(userId);
            
            return NoContent();
        }

        [HttpPut("SignOut/{userId}")]
        public async Task<IActionResult> SignOutUser(int userId)
        {
            if (!_userService.UserExists(userId)) return NotFound();

            await _userService.SignOut(userId);

            return NoContent();
        }

        // validationresult?
    }
}