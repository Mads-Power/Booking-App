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
    public class SeatController : Controller
    {
        private readonly IMapper _mapper;
        private readonly OfficeService _officeService;
        private readonly UserService _userService;
        private readonly RoomService _roomService;
        private readonly SeatService _seatService;
        private readonly BookingService _bookingService;

        public SeatController(IMapper mapper, OfficeService officeService, UserService userService, RoomService roomService, SeatService seatService, BookingService bookingService)
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
        ///     Fetch all seats from the database.
        /// </summary>
        /// <returns>A list of seat read DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<List<SeatReadDTO>>> GetAllSeats()
        {
            var seats = await _seatService.GetAllSeats();

            return _mapper.Map<List<SeatReadDTO>>(seats);
        }

        /// <summary>
        ///     Fetch a seat from the database based on seat id.
        /// </summary>
        /// <param name="seatId">The id of the seat.</param>
        /// <returns>
        ///     A read DTO of the seat if it is found in the database.
        ///     NotFound if it is not found.
        /// </returns>
        [HttpGet("{seatId}")]
        public async Task<ActionResult<SeatReadDTO>> GetSeat(int seatId)
        {
            try
            {
                var seat = await _seatService.GetSeatAsync(seatId);

                return _mapper.Map<SeatReadDTO>(seat);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///     Post a new seat to the database.
        /// </summary>
        /// <param name="dtoSeat">The create DTO for the seat.</param>
        /// <returns>
        ///     A read DTO of the seat which was created.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<SeatCreateDTO>> PostSeat(SeatCreateDTO dtoSeat)
        {
            // validate request and if not validated return BadRequest()?

            // if room does not exist bad request?

            var domainSeat = _mapper.Map<Seat>(dtoSeat);

            await _seatService.AddAsync(domainSeat);

            return CreatedAtAction("GetSeat",
                new { seatId = domainSeat.Id },
                _mapper.Map<SeatReadDTO>(domainSeat));
        }

        /// <summary>
        ///     Edit an existing seat in the database.
        /// </summary>
        /// <param name="seatId">The id of the seat.</param>
        /// <param name="seatDto">The edit DTO for the seat.</param>
        /// <returns>
        ///     BadRequest if body is invalid.
        ///     NotFound if id is invalid.
        ///     NoContent if seat was successfully updated. 
        /// </returns>
        [HttpPut("seatId")]
        public async Task<IActionResult> PutSeat(int seatId, SeatEditDTO seatDto)
        {
            var validation = ValidateUpdateSeat(seatDto, seatId);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
            }

            var domainSeat = await _seatService.GetSeatAsync(seatId);

            if (domainSeat != null)
            {
                _mapper.Map<SeatEditDTO,Seat>(seatDto,domainSeat);
            }
            else
            {
                return NotFound();
            }

            try
            {
                await _seatService.UpdateAsync(domainSeat);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        ///     Delete a seat from the database.
        /// </summary>
        /// <param name="seatId">Id of the seat.</param>
        /// <returns>
        ///     NotFound if id does not match anything in db.
        ///     NoContent if delete was successful.
        /// </returns>
        [HttpDelete("seatId")]
        public async Task<IActionResult> DeleteSeat(int seatId)
        {
            if (!_seatService.SeatExists(seatId))
            {
                return NotFound();
            }

            await _seatService.DeleteAsync(seatId);

            return NoContent();
        }

        private ValidationResult ValidateUpdateSeat(SeatEditDTO seatDto, int endpoint)
        {
            if (endpoint != seatDto.Id)
            {
                return new ValidationResult(false, "API endpoint and seat id must match");
            }

            if (!_roomService.RoomExists(seatDto.RoomId))
            {
                return new ValidationResult(false, "Given room does not exist");
            }

            // TODO: Improve validation with more checks, below is deprecated, find replacements
            //if (seatDto.IsOccupied && seatDto.UserId == null)
            //{
            //    return new ValidationResult(false, "Cannot be occupied without a valid user");
            //}

            //if (seatDto.UserId != null && !seatDto.IsOccupied)
            //{
            //    return new ValidationResult(false, "Cannot have a user without being occupied");
            //}

            return new ValidationResult(true);
        }
    }
}