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
using Microsoft.EntityFrameworkCore;

namespace Booking.Controllers
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

        public SeatController(IMapper mapper, OfficeService officeService, UserService userService, RoomService roomService, SeatService seatService)
        {
            _mapper = mapper;
            _officeService = officeService;
            _userService = userService;
            _roomService = roomService;
            _seatService = seatService;
        }

        // HTTP requests

        // get: all seats
        [HttpGet]
        public async Task<ActionResult<List<SeatReadDTO>>> GetAllSeats()
        {
            var seats = await _seatService.GetAllSeats();

            return _mapper.Map<List<SeatReadDTO>>(seats);
        }

        // get: seat info
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

        // post: add new seat
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

        // put: ...
        [HttpPut("seatId")]
        public async Task<IActionResult> PutSeat(int seatId, SeatEditDTO seatDto)
        {
            if (seatId != seatDto.Id)
            {
                return BadRequest();
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

        // delete: delete seat
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
    }
}