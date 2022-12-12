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
    public class OfficeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOfficeRepository _officeRepository;

        public OfficeController(IMapper mapper, IOfficeRepository officeRepository)
        {
            _mapper = mapper;
            _officeRepository = officeRepository;
        }

        // HTTP requests

        /// <summary>
        ///     Fetch all offices from the database.
        /// </summary>
        /// <returns>A list of office read DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<List<OfficeReadDTO>>> GetAllOffices()
        {
            var offices = await _officeRepository.GetOfficesAsync();

            return _mapper.Map<List<OfficeReadDTO>>(offices);
        }

        /// <summary>
        ///     Fetch an office from the database based on id.
        /// </summary>
        /// <param name="officeId">The id of the office.</param>
        /// <returns>
        ///     A read DTO of the office if it is found in the database.
        ///     NotFound if it is not found.
        /// </returns>
        [HttpGet("{officeId}")]
        public async Task<ActionResult<OfficeReadDTO>> GetOffice(int officeId)
        {
            var office = await _officeRepository.GetOfficeAsync(officeId);

            if (office == null)
            {
                return NotFound();
            }

            return _mapper.Map<OfficeReadDTO>(office);
        }

        /// <summary>
        ///     Post a new office to the database.
        /// </summary>
        /// <param name="dtoOffice">The create DTO for the office.</param>
        /// <returns>
        ///     A read DTO of the office which was created.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<OfficeCreateDTO>> PostOffice(OfficeCreateDTO dtoOffice)
        {
            // validate request and if not validated return BadRequest()?
            var validation = ValidatePostOffice(dtoOffice);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
            }

            var domainOffice = _mapper.Map<Office>(dtoOffice);

            await _officeRepository.AddAsync(domainOffice);

            return CreatedAtAction("GetOffice",
                new { officeId = domainOffice.Id },
                _mapper.Map<OfficeReadDTO>(domainOffice));
        }

        /// <summary>
        ///     Edit an existing office in the database.
        /// </summary>
        /// <param name="officeId">The id of the office.</param>
        /// <param name="officeDto">The edit DTO for the office.</param>
        /// <returns>
        ///     BadRequest if body is invalid.
        ///     NotFound if id is invalid.
        ///     NoContent if office was successfully updated. 
        /// </returns>
        [HttpPut("officeId")]
        public async Task<IActionResult> PutOffice(int officeId, OfficeEditDTO officeDto)
        {
            var validation = ValidateUpdateUser(officeDto, officeId);

            if (!validation.Result)
            {
                return BadRequest(validation.RejectionReason);
            }

            var domainOffice = await _officeRepository.GetOfficeAsync(officeId);

            if (domainOffice != null)
            {
                _mapper.Map<OfficeEditDTO,Office>(officeDto,domainOffice);
            }
            else
            {
                return NotFound();
            }

            try
            {
                await _officeRepository.UpdateAsync(domainOffice);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            
            return NoContent();
        }

        /// <summary>
        ///     Delete an office from the database.
        /// </summary>
        /// <param name="officeId">Id of the office.</param>
        /// <returns>
        ///     NotFound if id does not match anything in db.
        ///     NoContent if delete was successful.
        /// </returns>
        [HttpDelete("officeId")]
        public async Task<IActionResult> DeleteOffice(int officeId)
        {
            if (!_officeRepository.OfficeExists(officeId))
            {
                return NotFound();
            }

            await _officeRepository.DeleteAsync(officeId);

            return NoContent();
        }

        private static ValidationResult ValidateUpdateUser(OfficeEditDTO officeDto, int endpoint)
        {
            if (endpoint != officeDto.Id)
            {
                return new ValidationResult(false, "API endpoint and user id must match");
            }

            // more validation

            return new ValidationResult(true);
        }

        private ValidationResult ValidatePostOffice(OfficeCreateDTO officeDto)
        {
            if (_officeRepository.OfficeExists(officeDto.Id))
            {
                return new ValidationResult(false, "Office Id already exists");
            }

            return new ValidationResult(true);
        }
    }
}

