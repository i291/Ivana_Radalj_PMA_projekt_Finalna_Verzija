//using System;
//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Mvc;
//using MvcDemo.Models.AuthModel;
//using MvcDemo.Services;

//namespace MvcDemo.Controllers
//{
//    [Route("api/authentication")]
//    [ApiController]
//    public class AuthApiController : ControllerBase
//    {
//        private TravelService _travelService;
//        public AuthApiController(TravelService travelservice)
//        {
//            _travelService = travelservice;
//        }
//        public ClaimsPrincipal GetClaimsPrincipal(LoginModel user, bool isPersistent = false)
//        {
//            var claims = new List<Claim>()
//            {
//                //new Claim(ClaimTypes.Name, user.Email),
//                //new Claim(ClaimTypes.Role, user.RoleId.ToString())
//            };

//            var claimsIdentity = new ClaimsIdentity(
//                claims, "travel");

//            return new ClaimsPrincipal(claimsIdentity);
//        }


//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterData registerData)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var authProperties = new AuthenticationProperties
//            {
//                AllowRefresh = true,
//                ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(900),
//                IsPersistent = true,
//            };

//            var user = new LoginModel
//            {
//                Username = registerData.Username,
//                Password = registerData.Password
//            };
//            var cp = GetClaimsPrincipal(user);

//            await HttpContext.SignInAsync("travel",
//                cp, authProperties);

//            return Ok();
//        }


       
//    }
//}
