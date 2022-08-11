using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcDemo.DbModels;
using MvcDemo.Mappers;
using MvcDemo.Models.AuthModel;
using MvcDemo.Services;
using MvcDemo.Models;
namespace MvcDemo.Controllers
{
    public class AuthenticationController : Controller
    {
        private TravelService _travelService;
        public AuthenticationController(TravelService travelservice)
        {
            _travelService = travelservice;
        }
        

        public IActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginModel registerData) // provejra imam li usera u bazi odnosno jeli se vec registrira ili nije!
        {

            var user = await _travelService.getuser(registerData.Username, registerData.Password); //dohvati usera iz baze
            if(user == null)
            {
                return BadRequest(); // ako ga nema u bazi nemogu se logirat vrati mi bad rikvest
            }
            await SignIn(user); // ako ga ima u bazi pusti ga dase logira!
           
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {

           
            await SignOut();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterData registerData)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = UserMapper.ToDatabase(registerData);
            var isInserted = await _travelService.InsertUser(user); // jeli u bazi ili nije,ako nije insertat ce se preko repozitorija!

            if (isInserted != false) // ako je insertan u bazi vec daj mu da se prijavi!
            {
                await SignIn(user);
                return Ok();
            }
            else
            {
                return BadRequest("Not valid");
                

            }
        }

        private async Task SignIn(TblUser user)
        {
            var authProperties = new AuthenticationProperties
            {

                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(900),
                IsPersistent = true,
            };


            var cp = GetClaimsPrincipal(user);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                cp, authProperties); // logiraj ga i autentificiraj!
        }
        private async Task SignOut()
        {
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(-1), // postavi kuki na krivi kuki,na onaj od jučer tako da bude nevažeći da ga odma izbaci van aplikacije tj log outa!
                IsPersistent = true,
            };
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, this.User,
               authProperties); // logirali smo ga s krivim kukijem --> sign out
        }

        public ClaimsPrincipal GetClaimsPrincipal(TblUser user, bool isPersistent = false)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                 new Claim(ClaimTypes.Role, user.IsAdmin ? "ADMIN":"USER")
        };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);

        }
    }
}