using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingApp.Models.Domain;
using BookingApp.Models.DTOs;
using BookingApp.Repositories;
using BookingApp.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet("Me")]
        public async Task<ActionResult<ADUserDTO>> GetMe()
        {
            if (!User?.Identity?.IsAuthenticated ?? false) return Forbid();

            return new ADUserDTO
            {
                Name = User?.FindFirst("name")?.Value,
                GivenName = User?.FindFirst("given_name")?.Value,
                FamilyName = User?.FindFirst("family_name")?.Value,
                Email = User?.FindFirst("email")?.Value,
            };
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