using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using SuperHeroAPI.Services.SuperHeroService;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;

        public SuperHeroController(ISuperHeroService superHeroService)
        {
            _superHeroService = superHeroService;
        }

        [HttpGet]
        [Tags("Get All Heroes")]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {          
            return await _superHeroService.GetAllHeroes();
        }

        [HttpGet("{id}")]
        [Tags("Get Single Hero By Id")]
        public async Task<ActionResult<SuperHero>> GetSingleHero(int id)
        {
            var result = await _superHeroService.GetSingleHero(id);
            if (result is null)
                return NotFound("Hero not found");

            return Ok(result);
        }

        [HttpPost]
        [Tags("Add Hero")]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            var result = await _superHeroService.AddHero(hero);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Tags("Update Hero")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, SuperHero request)
        {
            var result = await _superHeroService.UpdateHero(id,request);
            if (result is null)
                return NotFound("Hero not found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Tags("Delete Hero")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
           var result = await _superHeroService.DeleteHero(id);
            if (result is null)
                return NotFound("Hero not found");
           
            return Ok(result);
        }
    }
}
