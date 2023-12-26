using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Server.Mapper;
using MadExpenceTracker.Server.Model;
using Microsoft.AspNetCore.Mvc;

namespace MadExpenceTracker.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class ExpenceController : ControllerBase
    {
        private readonly IExpencesService _service;

        public ExpenceController(IExpencesService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/expence")]
        public IActionResult CreateExpence([FromBody] ExpenceApi expenceApi)
        {
            return StatusCode(201, ExpencesMapper.MapToApi(_service.Create(ExpencesMapper.MapToModel(expenceApi))) );
        }

        [HttpPut]
        [Route("/expence")]
        public IActionResult UpdateExpence([FromBody] ExpenceApi expenceApi)
        {
            return StatusCode(202, _service.Update(ExpencesMapper.MapToModel(expenceApi)));
        }

        [HttpGet]
        [Route("/expence/{id}")]
        public IActionResult GetExpence(Guid id)
        {
            return Ok( ExpencesMapper.MapToApi(_service.GetExpence(id)) );
        }

        [HttpDelete]
        [Route("/expence/{id}")]
        public IActionResult DeleteExpence(Guid id)
        {
            return Ok(_service.Delete(id));
        }

        [HttpGet]
        [Route("/expences")]
        public IActionResult GetExpences()
        {
            return StatusCode( 200, ExpencesMapper.MapToApi(_service.GetAll()) );
        }

        [HttpGet]
        [Route("/expences/{id}")]
        public IActionResult GetExpencesById(Guid id)
        {
            return StatusCode( 200, ExpencesMapper.MapToApi(_service.GetAll().First(e => e.Id == id)) );
        }

        [HttpGet]
        [Route("/expences/current")]
        public IActionResult GetCurrentExpences()
        {
            return StatusCode( 200, ExpencesMapper.MapToApi(_service.GetExpences(true)) );
        }
    }
}
