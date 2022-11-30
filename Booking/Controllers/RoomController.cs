﻿using System;
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
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;

        public RoomController(IMapper mapper, RoomRepository roomRepository)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
        }

        // HTTP requests

        /// <summary>
        ///     Fetch all rooms from the database.
        /// </summary>
        /// <returns>A list of room read DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<List<RoomReadDTO>>> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAllRooms();

            return _mapper.Map<List<RoomReadDTO>>(rooms);
        }

        /// <summary>
        ///     Fetch a room from the database based on room id.
        /// </summary>
        /// <param name="roomId">The id of the room.</param>
        /// <returns>
        ///     A read DTO of the room if it is found in the database.
        ///     NotFound if it is not found.
        /// </returns>
        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomReadDTO>> GetRoom(int roomId)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);

            if (room == null)
            {
                return NotFound();
            }

            return _mapper.Map<RoomReadDTO>(room);
        }

        /// <summary>
        ///     Post a new room to the database.
        /// </summary>
        /// <param name="dtoRoom">The create DTO for the room.</param>
        /// <returns>
        ///     A read DTO of the room which was created.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<RoomCreateDTO>> PostRoom(RoomCreateDTO dtoRoom)
        {
            // validate request and if not validated return BadRequest()?

            var domainRoom = _mapper.Map<Room>(dtoRoom);

            await _roomRepository.AddAsync(domainRoom);

            return CreatedAtAction("GetRoom",
                new { roomId = domainRoom.Id },
                _mapper.Map<RoomReadDTO>(domainRoom));
        }

        /// <summary>
        ///     Edit an existing room in the database.
        /// </summary>
        /// <param name="roomId">The id of the room.</param>
        /// <param name="roomDto">The edit DTO for the room.</param>
        /// <returns>
        ///     BadRequest if body is invalid.
        ///     NotFound if id is invalid.
        ///     NoContent if room was successfully updated. 
        /// </returns>
        [HttpPut("roomId")]
        public async Task<IActionResult> PutRoom(int roomId, RoomEditDTO roomDto)
        {
            var validation = ValidateUpdateRoom(roomDto, roomId);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
            }

            var domainRoom = await _roomRepository.GetRoomAsync(roomId);

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
                await _roomRepository.UpdateAsync(domainRoom);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        ///     Delete a room from the database.
        /// </summary>
        /// <param name="roomId">Id of the room.</param>
        /// <returns>
        ///     NotFound if id does not match anything in db.
        ///     NoContent if delete was successful.
        /// </returns>
        [HttpDelete("roomId")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            if (!_roomRepository.RoomExists(roomId))
            {
                return NotFound();
            }

            await _roomRepository.DeleteAsync(roomId);

            return NoContent();
        }

        /// <summary>
        ///     Get all seats in a room.
        /// </summary>
        /// <param name="roomId">Id of the room.</param>
        /// <returns>
        ///     A list of seat read DTOs.
        ///     NotFound if room id is invalid.
        /// </returns>
        [HttpGet("{roomId}/Seats")]
        public async Task<ActionResult<List<SeatReadDTO>>> GetSeatsInRoom(int roomId)
        {
            try
            {
                var seats = await _roomRepository.GetSeatsInRoom(roomId);

                return _mapper.Map<List<SeatReadDTO>>(seats);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///     Get all users with seats booked in the room.
        /// </summary>
        /// <param name="roomId">If of the room.</param>
        /// <returns>
        ///     List of user read DTOs.
        ///     NotFound if room id is invalid.
        /// </returns>
        [HttpGet("{roomId}/Users")]
        public async Task<ActionResult<List<UserReadDTO>>> GetUsersInRoom(int roomId)
        {
            try
            {
                var users = await _roomRepository.GetSignedInUsersInRoom(roomId);

                return _mapper.Map<List<UserReadDTO>>(users);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        private ValidationResult ValidateCreateRoom(RoomCreateDTO roomDto)
        {
            // see if room with that id already exists
            //if (_roomRepository.RoomExists(roomDto.Id) {
            //    return new ValidationResult(false, "Please provide a unique Id")
            //}

            // see if officeId does not exist
            //if (roomDto.)

            return new ValidationResult(true);
        }

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