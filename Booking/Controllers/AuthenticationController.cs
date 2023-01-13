using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
	{
		public AuthenticationController()
		{
		}


        public static ActionResult Login(string returnUrl)
        {
            return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        RedirectUri = returnUrl ?? "/"
                    });
        }
    }
}

