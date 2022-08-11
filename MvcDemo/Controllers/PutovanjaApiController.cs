using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using MvcDemo.Models;
using MvcDemo.DtoMappers;
using MvcDemo.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;

namespace MvcDemo.Controllers
{
    [Route("api/putovanja")]
    [ApiController]
    public class PutovanjaApiController : Controller
    {

        private TravelService _travelService;
        public PutovanjaApiController(TravelService travelservice)
        {
            _travelService = travelservice;
        }
        

        [HttpGet]
        public ActionResult<List<Putovanje>> Get()
        {
            //return _listaputovanja;
            return _travelService.GetAll().ToList();

        }

        [HttpGet("{id}")] // api/putovanja/0 --> za edit page
        public async Task<ActionResult<Putovanje>> Get(int id)
        {

            return await _travelService.GetAsync(id);
        }
        [HttpPut("edittravel")]
        public void Edit([FromBody] JObject json)
        {
            var travel = PDto.FromJson(json);
           
            _travelService.Edit(travel);
            
            
        }

        [HttpPost("savetravel2")]
        public ActionResult Save([FromBody] JObject json)
        {
            var travel = PDto.FromJson(json); // iz jsona pretvara u Putovanje "norrmalno"!
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
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
             _travelService.DeleteAsync(id);
            


        }
        //za dodavanje favorita!
        [HttpPost("addfavorite")]
        public async Task<ActionResult> AddFavorite([FromBody]  JObject json)
        {
            if (await _travelService.Saveusertravel(json[ "travelid"].ToObject<int>(), int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value)))
            {
                return Ok();

            }
            return NoContent() ;

        }
        [HttpDelete("removefavorite/{id}")]
        public async Task<ActionResult> Removefavorite(int id)
        {

            await _travelService.Deleteusertravel(id, int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
           

        }



    }
}