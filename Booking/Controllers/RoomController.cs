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
    public class RoomController : Controller
    {
        private readonly IMapper _mapper;
        private readonly OfficeService _officeService;
        private readonly UserService _userService;
        private readonly RoomService _roomService;
        private readonly SeatService _seatService;

        public RoomController(IMapper mapper, OfficeService officeService, UserService userService, RoomService roomService, SeatService seatService)
        {
            _mapper = mapper;
            _officeService = officeService;
            _userService = userService;
            _roomService = roomService;
            _seatService = seatService;
        }

        // HTTP requests

        // get: all rooms
        [HttpGet]
        public async Task<ActionResult<List<RoomReadDTO>>> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRooms();

            return _mapper.Map<List<RoomReadDTO>>(rooms);
        }

        // get: room info
        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomReadDTO>> GetRoom(int roomId)
        {
            try
            {
                var room = await _roomService.GetRoomAsync(roomId);

                return _mapper.Map<RoomReadDTO>(room);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        // post: add new room
        [HttpPost]
        public async Task<ActionResult<RoomCreateDTO>> PostRoom(RoomCreateDTO dtoRoom)
        {
            // validate request and if not validated return BadRequest()?

            var domainRoom = _mapper.Map<Room>(dtoRoom);

            await _roomService.AddAsync(domainRoom);

            return CreatedAtAction("GetRoom",
                new { roomId = domainRoom.Id },
                _mapper.Map<RoomReadDTO>(domainRoom));
        }

        // put: ...
        [HttpPut("roomId")]
        public async Task<IActionResult> PutRoom(int roomId, RoomEditDTO roomDto)
        {
            var validation = ValidateUpdateRoom(roomDto, roomId);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
            }

            var domainRoom = await _roomService.GetRoomAsync(roomId);

            if (domainRoom != null)
            {
                _mapper.Map<RoomEditDTO,Room>(roomDto,domainRoom);
            }
            else
            {
                return NotFound();
            }

            try
            {
                await _roomService.UpdateAsync(domainRoom);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // delete: delete room
        [HttpDelete("roomId")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            if (!_roomService.RoomExists(roomId))
            {
                return NotFound();
            }

            await _roomService.DeleteAsync(roomId);

            return NoContent();
        }

        // get all seats in the room
        [HttpGet("{roomId}/Seats")]
        public async Task<ActionResult<List<SeatReadDTO>>> GetSeatsInRoom(int roomId)
        {
            try
            {
                var seats = await _roomService.GetSeatsInRoom(roomId);

                return _mapper.Map<List<SeatReadDTO>>(seats);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        // get: sigend in users in room
        [HttpGet("{roomId}/Users")]
        public async Task<ActionResult<List<UserReadDTO>>> GetUsersInRoom(int roomId)
        {
            try
            {
                var users = await _roomService.GetSignedInUsersInRoom(roomId);

                return _mapper.Map<List<UserReadDTO>>(users);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        private static ValidationResult ValidateCreateSe

        private static ValidationResult ValidateUpdateRoom(RoomEditDTO roomDto, int endpoint)
        {
            if (endpoint != roomDto.Id)
            {
                return new ValidationResult(false, "API endpoint and room id must match");
            }

            // more validation

            return new ValidationResult(true);
        }
    }
}