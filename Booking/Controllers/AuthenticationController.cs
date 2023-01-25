using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
    [AllowAnonymous]
    [Route("Account/Login")]
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
        public ActionResult Login(string? returnUri = "/")
        {
            return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        RedirectUri = returnUri ?? "/"
                    });
        }
    }
}

