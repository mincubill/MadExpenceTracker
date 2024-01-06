using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Server.Mapper;
using MadExpenceTracker.Server.Model;
using Microsoft.AspNetCore.Mvc;

namespace MadExpenceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthIndexController : ControllerBase
    {
        private readonly IMonthIndexingService _service;

        public MonthIndexController(IMonthIndexingService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/monthIndex")]
        public IActionResult GetMonthIndex()
        {
            var res = MonthIndexMapper.MapToApi(_service.GetMonthsIndexes());
            return Ok(res);
        }

        [HttpPost]
        [Route("/monthIndex")]
        public IActionResult SaveMonthIndex([FromBody] MonthIndexApi monthIndex)
        {
            return Ok(MonthIndexMapper.MapToApi(
                _service.AddMonthIndex(
                    MonthIndexMapper.MapToModel(monthIndex)
                    )
                ));
        }
    }
}
