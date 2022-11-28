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
using BookingApp.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly OfficeService _officeService;
        private readonly UserService _userService;
        private readonly RoomService _roomService;
        private readonly SeatService _seatService;
        private readonly BookingService _bookingService;

        public BookingController(IMapper mapper, OfficeService officeService, UserService userService, RoomService roomService, SeatService seatService, BookingService bookingService)
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
        ///     Fetch all bookings from the database.
        /// </summary>
        /// <returns>A list of booking read DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<List<BookingReadDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookings();

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
            try
            {
                var booking = await _bookingService.GetBookingAsync(bookingId);

                return _mapper.Map<BookingReadDTO>(booking);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///     Post a new booking to the database.
        /// </summary>
        /// <param name="dtoBooking">The create DTO for the booking.</param>
        /// <returns>
        ///     A read DTO of the booking which was created.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<BookingCreateDTO>> PostBooking(BookingCreateDTO dtoBooking)
        {
            // validate request and if not validated return BadRequest()?

            // if room does not exist bad request?

            var domainBooking = _mapper.Map<Booking>(dtoBooking);

            await _bookingService.AddAsync(domainBooking);

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

            var domainBooking = await _bookingService.GetBookingAsync(bookingId);

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
                await _bookingService.UpdateAsync(domainBooking);
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
            if (!_bookingService.BookingExists(bookingId))
            {
                return NotFound();
            }

            await _bookingService.DeleteAsync(bookingId);

            return NoContent();
        }

        private ValidationResult ValidateUpdateBooking(BookingEditDTO bookingDto, int endpoint)
        {
            // validation

            return new ValidationResult(true);
        }
    }
}

