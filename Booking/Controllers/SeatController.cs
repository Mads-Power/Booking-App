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
    public class SeatController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;
        private readonly ISeatRepository _seatRepository;

        public SeatController(IMapper mapper, IRoomRepository roomRepository, ISeatRepository seatRepository)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
            _seatRepository = seatRepository;
        }

        // HTTP requests

        /// <summary>
        ///     Fetch all seats from the database.
        /// </summary>
        /// <returns>A list of seat read DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<List<SeatReadDTO>>> GetAllSeats()
        {
            var seats = await _seatRepository.GetSeatsAsync();
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
            var seat = await _seatRepository.GetSeatAsync(seatId);

            if (seat == null)
            {
                return NotFound();
            }

            seat.Bookings.ForEach(b => b.Date = b.Date.ToLocalTime());

            return _mapper.Map<SeatReadDTO>(seat);
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
            var validation = ValidatePostSeat(dtoSeat);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
            }

            var domainSeat = _mapper.Map<Seat>(dtoSeat);

            await _seatRepository.AddAsync(domainSeat);

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

            var domainSeat = await _seatRepository.GetSeatAsync(seatId);

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
                await _seatRepository.UpdateAsync(domainSeat);
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
            if (!_seatRepository.SeatExists(seatId))
            {
                return NotFound();
            }

            await _seatRepository.DeleteAsync(seatId);

            return NoContent();
        }

        private ValidationResult ValidateUpdateSeat(SeatEditDTO seatDto, int endpoint)
        {
            if (endpoint != seatDto.Id)
            {
                return new ValidationResult(false, "API endpoint and seat id must match");
            }

            if (!_roomRepository.RoomExists(seatDto.RoomId))
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

        private ValidationResult ValidatePostSeat(SeatCreateDTO seatDto)
        {
            if (!_roomRepository.RoomExists(seatDto.RoomId))
            {
                return new ValidationResult(false, "Cannot match room");
            }

            if (_seatRepository.SeatExists(seatDto.Id))
            {
                return new ValidationResult(false, "Seat Id already exists");
            }

            return new ValidationResult(true);
        }
    }
}