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
using BookingApp.Repositories;
using BookingApp.Helpers;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UserController(IMapper mapper, IUserRepository userRepository, ISeatRepository seatRepository, IBookingRepository bookingRepository, IDateTimeProvider dateTimeProvider)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _seatRepository = seatRepository;
            _bookingRepository = bookingRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        // HTTP requests

        /// <summary>
        /// Fetch all users from the database.
        /// </summary>
        /// <returns>A list of User read DTOs </returns>
        [HttpGet]
        public async Task<ActionResult<List<UserReadDTO>>> GetAllUsers()
        {
            var users = await _userRepository.GetUsersAsync();

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
            var user = await _userRepository.GetUserAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.Bookings.ForEach(b => b.Date = b.Date.ToLocalTime());

            return _mapper.Map<UserReadDTO>(user);
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
            var validation = ValidatePostUser(dtoUser);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
            }

            var domainUser = _mapper.Map<User>(dtoUser);

            await _userRepository.AddAsync(domainUser);

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

            var domainUser = await _userRepository.GetUserAsync(userId);

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
                await _userRepository.UpdateAsync(domainUser);
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
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(userId);

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
        [HttpPut("{userId}/Book")]
        public async Task<IActionResult> UserBookSeat(int userId, [FromQuery] int seatId, [FromQuery] string date)
        {
            var dateValidation = ValidationResult.ValidateDateString(date);

            if (!dateValidation.Result)
            {
                return BadRequest(dateValidation.RejectionReason);
            }

            var validation = ValidateUserBookSeat(userId, seatId, date);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
            }

            var dateTime = _dateTimeProvider.Parse(date);

            var domainUser = await _userRepository.GetUserAsync(userId);
            var domainSeat = await _seatRepository.GetSeatAsync(seatId);

            if (domainUser == null || domainSeat == null)
            {
                return NotFound();
            }

            try
            {
                await _bookingRepository.BookSeat(domainUser, domainSeat, dateTime);
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
        /// <param name="date">Date of the booking.</param>
        /// <returns>
        ///     NotFound if the ids don't match.
        ///     NoContent if seat was successfully unbooked.
        /// </returns>
        [HttpPut("{userId}/Unbook")]
        public async Task<IActionResult> UserUnbookSeat(int userId, [FromQuery] string date)
        {
            var dateValidation = ValidationResult.ValidateDateString(date);

            if (!dateValidation.Result)
            {
                return BadRequest(dateValidation.RejectionReason);
            }

            var validation = ValidateUserUnbookSeat(userId, date);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
            }

            var dateTime = _dateTimeProvider.Parse(date);

            var domainUser = await _userRepository.GetUserAsync(userId);

            if (domainUser == null)
            {
                return NotFound();
            }

            try
            {
                await _bookingRepository.UnbookSeat(domainUser, dateTime);
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

        private ValidationResult ValidateUserBookSeat(int userId, int seatId, string date)
        {
            // validate date input
            DateTime dateTime;

            try
            {
                dateTime = _dateTimeProvider.Parse(date);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Given date is not recognized as valid");
            }

            // validate that date is not in the past
            if (_dateTimeProvider.Compare(dateTime.Date, _dateTimeProvider.Today().Date) < 0)
            {
                return new ValidationResult(false, "Cannot book for past dates");
            }


            // validate if seat is taken that day
            if (_bookingRepository.GetBookingByDateAndSeat(dateTime,seatId) != null)
            {
                return new ValidationResult(false, "Seat already booked that day");
            }

            // validate if user already booked a seat that day
            if (_bookingRepository.GetBookingByDateAndUser(dateTime, userId) != null)
            {
                return new ValidationResult(false, "User already booked that day");
            }

            return new ValidationResult(true);
        }

        // validate user unbook seat
        private ValidationResult ValidateUserUnbookSeat(int userId, string date)
        {
            // validate date input
            DateTime dateTime;

            try
            {
                dateTime = _dateTimeProvider.Parse(date);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Given date is not recognized as valid");
            }

            // validate booking exists
            if (_bookingRepository.GetBookingByDateAndUser(dateTime, userId) == null)
            {
                return new ValidationResult(false, "No booking found for the user on that day");
            }

            return new ValidationResult(true);
        }

        private ValidationResult ValidatePostUser(UserCreateDTO userDto)
        {
            if (_userRepository.UserExists(userDto.Id))
            {
                return new ValidationResult(false, "User Id already exists");
            }

            if (userDto.Name == null)
            {
                return new ValidationResult(false, "Must provide a name for the user.");
            }
            return new ValidationResult(true);
        }
    }
}