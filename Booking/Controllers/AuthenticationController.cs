using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
    [Route("api/authenticate")]
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
        public ActionResult Authenticate(string returnUri = "/")
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

