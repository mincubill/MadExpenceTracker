using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Server.Mapper;
using MadExpenceTracker.Server.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadExpenceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmountController : ControllerBase
    {
        private readonly IAmountsService _service;

        public AmountController(IAmountsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/amount/{expenceId}/{incomeId}")]
        public IActionResult GetAmount(Guid expenceId, Guid incomeId)
        {
            return Ok(AmountMapper.MapToApi(_service.GetAmount(expenceId, incomeId)));
        }

        [HttpGet]
        [Route("/amounts/{id}")]
        public IActionResult GetAmount(Guid id)
        {
            return Ok(AmountMapper.MapToApi(_service.GetAmount(id)));
        }

        [HttpPost]
        public IActionResult SaveAmounts([FromBody]AmountApi amount)
        {
            return Ok(AmountMapper.MapToApi(_service.Create(AmountMapper.MapToModel(amount))));
        }

        [HttpGet]
        [Route("/amounts")]
        public IActionResult GetAmounts()
        {
            return Ok(AmountMapper.MapToApi(_service.GetAmounts()));
        }

        
    }
}
