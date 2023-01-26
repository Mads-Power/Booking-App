using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BookingApp.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AuthenticationController : Controller
	{

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
        [AllowAnonymous]
        [Route("IsAuthenticated")]
        public ActionResult IsAuthenticated()
        {
            if (User?.Identity?.IsAuthenticated is not null && User.Identity.IsAuthenticated)
                return Ok();

            return Unauthorized();
        }
    }
}

