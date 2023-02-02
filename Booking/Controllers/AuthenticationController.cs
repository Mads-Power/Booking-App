using BookingApp.Models.Domain;
using BookingApp.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace BookingApp.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AuthenticationController : Controller
	{
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Challenge the request from the user
        /// <summary>
        /// Challenge the authentication request from the User.
        /// </summary>
        /// <param name="returnUri"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("Login")]
        public ActionResult Login(string? returnUri = "/")
        {
            return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        RedirectUri = returnUri ?? "/"
                    });
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            List<string> schemes = new () { OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme };

            return new SignOutResult(schemes, new AuthenticationProperties
            {
                IsPersistent = true,
                RedirectUri = "/login"
            });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("IsAuthenticated")]
        public async Task<ActionResult> IsAuthenticated()
        {
            if (User?.Identity?.IsAuthenticated is not null && User.Identity.IsAuthenticated)
            {
                if(await IsUserFoundOrCreated())
                {
                    return Ok();
                }             
            }

            return Unauthorized();
        }

        private async Task<bool> IsUserFoundOrCreated()
        {
            var userId = User?.FindFirst(ClaimConstants.ObjectId)?.Value;
            var userEmail = User?.FindFirst("email")?.Value;

            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("Could not find userID");
            if (string.IsNullOrWhiteSpace(userEmail)) throw new ArgumentNullException("Could not find user email");

            if (_userRepository.UserExists(userEmail))            
                return true;             

            await _userRepository.AddAsync(new User
            {
                Id = userId,
                Name = User?.FindFirst(ClaimConstants.Name)?.Value ?? "NoName",
                Email = User?.FindFirst("email")?.Value ?? "NoEmail"
            });

            if (_userRepository.UserExists(userEmail)) 
                return true;


            return false;
        }
    }
}

