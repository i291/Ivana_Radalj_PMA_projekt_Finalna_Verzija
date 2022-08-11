using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using MvcDemo.Models;
using MvcDemo.DtoMappers;
using MvcDemo.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MvcDemo.Controllers
{
    [Route("api/MOJAPUTOVANJA")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private TravelService _travelService;
        public UserApiController(TravelService travelservice)
        {
            _travelService = travelservice;
        }


        [HttpGet]
        public ActionResult<List<Putovanje>> Get()
        {
            //return _listaputovanja;
            return _travelService.GetAll().ToList();

        }
       [HttpGet("moji")]
       [Authorize]
        public ActionResult<List<Putovanje>> Gett()
        {
            ClaimsPrincipal currentuser = this.User;
           return _travelService.GetAlll( int.Parse( currentuser.FindFirst(ClaimTypes.NameIdentifier).Value)).ToList();



        }

        [HttpGet("{id}")] // api/MOJAPUTOVANJA/0
        public async Task<ActionResult<Putovanje>> Get(int id)
        {

            return await _travelService.GetAsync(id);
        }
       
        [HttpPost("savetravel2")]
        public ActionResult Save([FromBody] JObject json)
        {
            var travel = PDto.FromJson(json);
            _travelService.Save(travel);
            return Ok();
        }
        [HttpPost("savetravel3")]
        public ActionResult SaveandReturn([FromBody] JObject json)
        {
            var course = PDto.FromJson(json);
            var result = _travelService.SaveandReturn(course);
            return Ok(result);
        }
        
       
    }
}