using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingApp.Context;
using BookingApp.Models.Domain;
using BookingApp.Models.DTOs;
using BookingApp.Services;

namespace BookingApp.Controllers
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
        private readonly BookingService _bookingService;

        public UserController(IMapper mapper, OfficeService officeService, UserService userService, RoomService roomService, SeatService seatService, BookingService bookingService)
        {
            _mapper = mapper;
            _officeService = officeService;
            _userService = userService;
            _roomService = roomService;
            _seatService = seatService;
            _bookingService = bookingService;
        }

        // HTTP requests

        /// <summary>
        /// Fetch all users from the database.
        /// </summary>
        /// <returns>A list of User read DTOs </returns>
        [HttpGet]
        public async Task<ActionResult<List<UserReadDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return _mapper.Map<List<UserReadDTO>>(users);
        }

        /// <summary>
        /// Fetch a specific user from the database.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>A user read DTO.</returns>
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

        /// <summary>
        ///     Post a new user to the database.
        /// </summary>
        /// <param name="dtoUser">The create DTO for the user.</param>
        /// <returns>
        ///     A read DTO of the user that was created.
        /// </returns>
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

        /// <summary>
        ///     Edit an existing user in the database.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <param name="userDto">The edit DTO for the user.</param>
        /// <returns>
        ///     BadRequest if body is invalid.
        ///     NotFound if id is invalid.
        ///     NoContent if user was successfully updated. 
        /// </returns>
        [HttpPut("userId")]
        public async Task<IActionResult> PutUser(int userId, UserEditDTO userDto)
        {
            var validation = ValidateUpdateUser(userDto, userId);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
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

        /// <summary>
        ///     Delete a user from the database.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns>
        ///     NotFound if id does not match anything in db.
        ///     NoContent if delete was successful.
        /// </returns>
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

        /// <summary>
        ///     Book a seat for the user.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="seatId">Id of the seat.</param>
        /// <param name="date">Date of the booking.</param>
        /// <returns>
        ///     NotFound if the ids don't match.
        ///     NoContent if seat was successfully booked.
        /// </returns>
        [HttpPut("{userId}/Book/{seatId}")]
        public async Task<IActionResult> UserBookSeat(int userId, int seatId, [FromQuery] string date)
        {
            // validate book seat (fail if seat already booked that day) //validate date input, convert to utc etc.

            var dateTime = DateTime.Parse(date).ToUniversalTime();

            var domainUser = await _userService.GetUserAsync(userId);
            var domainSeat = await _seatService.GetSeatAsync(seatId);

            if (domainUser == null || domainSeat == null)
            {
                return NotFound();
            }

            try
            {
                await _bookingService.BookSeat(domainUser, domainSeat, dateTime);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        ///     Unbook a seat for the user.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="seatId">Id of the seat.</param>
        /// <param name="date">Date of the booking.</param>
        /// <returns>
        ///     NotFound if the ids don't match.
        ///     NoContent if seat was successfully unbooked.
        /// </returns>
        [HttpPut("{userId}/Unbook/{seatId}")]
        public async Task<IActionResult> UserUnbookSeat(int userId, int seatId, [FromQuery] string date)
        {
            // validate unbook seat (fail if seat already unbooked)

            var dateTime = DateTime.Parse(date).ToUniversalTime();

            var domainUser = await _userService.GetUserAsync(userId);
            var domainSeat = await _seatService.GetSeatAsync(seatId);

            if (domainUser == null || domainSeat == null)
            {
                return NotFound();
            }

            try
            {
                await _bookingService.UnbookSeat(domainUser, domainSeat, dateTime);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        private static ValidationResult ValidateUpdateUser(UserEditDTO userDto, int endpoint)
        {
            if (endpoint != userDto.Id)
            {
                return new ValidationResult(false, "API endpoint and user id must match");
            }

            // more validation

            return new ValidationResult(true);
        }
    }
}