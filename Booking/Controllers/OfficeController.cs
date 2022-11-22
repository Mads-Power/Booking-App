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

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly OfficeService _officeService;
        private readonly UserService _userService;
        private readonly RoomService _roomService;
        private readonly SeatService _seatService;

        public OfficeController(IMapper mapper, OfficeService officeService, UserService userService, RoomService roomService, SeatService seatService)
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
        public async Task<ActionResult<List<OfficeReadDTO>>> GetAllOffices()
        {
            var offices = await _officeService.GetAllOffices();

            return _mapper.Map<List<OfficeReadDTO>>(offices);
        }

        // get: office info
        [HttpGet("{officeId}")]
        public async Task<ActionResult<OfficeReadDTO>> GetOffice(int officeId)
        {
            try
            {
                var office = await _officeService.GetOfficeAsync(officeId);

                return _mapper.Map<OfficeReadDTO>(office);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        // post: add new office
        [HttpPost]
        public async Task<ActionResult<OfficeCreateDTO>> PostOffice(OfficeCreateDTO dtoOffice)
        {
            // validate request and if not validated return BadRequest()?

            var domainOffice = _mapper.Map<Office>(dtoOffice);

            await _officeService.AddAsync(domainOffice);

            return CreatedAtAction("GetOffice",
                new { officeId = domainOffice.Id },
                _mapper.Map<OfficeReadDTO>(domainOffice));
        }

        // put: ...
        [HttpPut("officeId")]
        public async Task<IActionResult> PutOffice(int officeId, OfficeEditDTO officeDto)
        {
            if (officeId != officeDto.Id)
            {
                return BadRequest();
            }

            if (!_officeService.OfficeExists(officeId))
            {
                return NotFound();
            }

            var domainOffice = _mapper.Map<Office>(officeDto);
            await _officeService.UpdateAsync(domainOffice);

            return NoContent();
        }

        // delete: delete office
        [HttpDelete("officeId")]
        public async Task<IActionResult> DeleteOffice(int officeId)
        {
            if (!_officeService.OfficeExists(officeId))
            {
                return NotFound();
            }

            await _officeService.DeleteAsync(officeId);

            return NoContent();
        }
    }
}

