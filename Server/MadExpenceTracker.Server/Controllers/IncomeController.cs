using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Server.Mapper;
using MadExpenceTracker.Server.Model;
using Microsoft.AspNetCore.Mvc;

namespace MadExpenceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _service;

        public IncomeController(IIncomeService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/income")]
        public IActionResult CreateIncome([FromBody] IncomeApi incomeApi) 
        {
            return StatusCode(201, IncomeMapper.MapToApi(_service.Create(IncomeMapper.MapToModel(incomeApi))));
        }

        [HttpPut]
        [Route("/income")]
        public IActionResult UpdateIncome([FromBody] IncomeApi incomeApi)
        {
            return StatusCode(202, _service.Update(IncomeMapper.MapToModel(incomeApi)) );
        }

        [HttpGet]
        [Route("/income/{id}")]
        public IActionResult GetIncome(Guid id)
        {
            return Ok(IncomeMapper.MapToApi(_service.GetIncome(id)));
        }

        [HttpGet]
        [Route("/incomes")]
        public IActionResult GetIncomes()
        {
            return StatusCode(200, IncomeMapper.MapToApi(_service.GetAll()));
        }

        [HttpDelete]
        [Route("/income/{id}")]
        public IActionResult DeleteIncome(Guid id)
        {
            return Ok(_service.Delete(id));
        }

        [HttpGet]
        [Route("/incomes/{id}")]
        public IActionResult GetIncomesById(Guid id)
        {
            return StatusCode(200, IncomeMapper.MapToApi(_service.GetAll().First(e => e.Id == id)));
        }

        [HttpGet]
        [Route("/incomes/current")]
        public IActionResult GetCurrentIncomes()
        {
            return StatusCode(200, IncomeMapper.MapToApi(_service.GetIncomes(true)));
        }
    }
}
