using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookingApp.Context;
using BookingApp.Models.Domain;
using BookingApp.Models.DTOs;
using BookingApp.Repositories;
using BookingApp.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public BookingController(IMapper mapper, IBookingRepository bookingRepository, IUserRepository userRepository, ISeatRepository seatRepository, IDateTimeProvider dateTimeProvider)
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _seatRepository = seatRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        // HTTP requests

        /// <summary>
        ///     Fetch all bookings from the database.
        /// </summary>
        /// <returns>A list of booking read DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<List<BookingReadDTO>>> GetAllBookings()
        {
            var bookings = await _bookingRepository.GetBookingsAsync();

            return _mapper.Map<List<BookingReadDTO>>(bookings);
        }

        /// <summary>
        ///     Fetch a booking from the database based on booking id.
        /// </summary>
        /// <param name="bookingId">The id of the booking.</param>
        /// <returns>
        ///     A read DTO of the booking if it is found in the database.
        ///     NotFound if it is not found.
        /// </returns>
        [HttpGet("{bookingId}")]
        public async Task<ActionResult<BookingReadDTO>> GetBooking(int bookingId)
        {
            var booking = await _bookingRepository.GetBookingAsync(bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            return _mapper.Map<BookingReadDTO>(booking);
        }

        /// <summary>
        ///     Post a new booking to the database.
        /// </summary>
        /// <param name="dtoBooking">The create DTO for the booking.</param>
        /// <returns>
        ///     A read DTO of the booking which was created.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<BookingReadDTO>> PostBooking(BookingCreateDTO dtoBooking)
        {
            // validate date // date is not in the past etc
            var dateValidation = ValidateDtoDate(dtoBooking.Date);

            if (!dateValidation.Result)
            {
                return BadRequest(dateValidation.RejectionReason);
            }

            var validation = ValidatePostBooking(dtoBooking);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
            }

            var domainBooking = _mapper.Map<Booking>(dtoBooking);

            domainBooking.Date = dtoBooking.Date.ToUniversalTime();

            await _bookingRepository.AddAsync(domainBooking);

            return CreatedAtAction("GetBooking",
                new { bookingId = domainBooking.Id },
                _mapper.Map<BookingReadDTO>(domainBooking));
        }

        /// <summary>
        ///     Edit an existing booking in the database.
        /// </summary>
        /// <param name="bookingId">The id of the booking.</param>
        /// <param name="bookingDto">The edit DTO for the booking.</param>
        /// <returns>
        ///     BadRequest if body is invalid.
        ///     NotFound if id is invalid.
        ///     NoContent if booking was successfully updated. 
        /// </returns>
        [HttpPut("bookingId")]
        public async Task<IActionResult> PutBooking(int bookingId, BookingEditDTO bookingDto)
        {
            var validation = ValidateUpdateBooking(bookingDto, bookingId);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
            }

            var domainBooking = await _bookingRepository.GetBookingAsync(bookingId);

            if (domainBooking != null)
            {
                _mapper.Map<BookingEditDTO, Booking>(bookingDto, domainBooking);
            }
            else
            {
                return NotFound();
            }

            try
            {
                await _bookingRepository.UpdateAsync(domainBooking);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        ///     Delete a booking from the database.
        /// </summary>
        /// <param name="bookingId">Id of the booking.</param>
        /// <returns>
        ///     NotFound if id does not match anything in db.
        ///     NoContent if delete was successful.
        /// </returns>
        [HttpDelete("bookingId")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            if (!_bookingRepository.BookingExists(bookingId))
            {
                return NotFound();
            }

            await _bookingRepository.DeleteAsync(bookingId);

            return NoContent();
        }

        private ValidationResult ValidateUpdateBooking(BookingEditDTO bookingDto, int endpoint)
        {
            if (endpoint != bookingDto.Id)
            {
                return new ValidationResult(false, "API endpoint and room id must match");
            }

            return new ValidationResult(true);
        }

        private ValidationResult ValidatePostBooking(BookingCreateDTO bookingDto)
        {
            if (!_userRepository.UserExists(bookingDto.UserId))
            {
                return new ValidationResult(false, "Cannot match user");
            }

            if (!_seatRepository.SeatExists(bookingDto.SeatId))
            {
                return new ValidationResult(false, "Cannot match seat");
            }

            if (_bookingRepository.GetBookingByDateAndUser(bookingDto.Date,bookingDto.UserId) != null)
            {
                return new ValidationResult(false, "Booking already exists for the user that day");
            }

            if (_bookingRepository.GetBookingByDateAndSeat(bookingDto.Date, bookingDto.SeatId) != null)
            {
                return new ValidationResult(false, "Booking already exists for the seat that day");
            }

            if (_bookingRepository.BookingExists(bookingDto.Id))
            {
                return new ValidationResult(false, "Booking Id already exists");
            }

            return new ValidationResult(true);
        }

        private ValidationResult ValidateDtoDate(DateTime dtoDate)
        {
            if (_dateTimeProvider.Compare(dtoDate.ToUniversalTime().Date, _dateTimeProvider.Today().Date) < 0)
            {
                return new ValidationResult(false, "Cannot book for past dates");
            }
            return new ValidationResult(true);
        }
    }
}

