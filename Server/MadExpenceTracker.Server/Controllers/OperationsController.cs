using MadExpenceTracker.Core.Interfaces.UseCase;
using MadExpenceTracker.Server.Mapper;
using MadExpenceTracker.Server.Model;
using Microsoft.AspNetCore.Mvc;

namespace MadExpenceTracker.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IMonthClose _monthClose;

        public OperationsController(IMonthClose monthClose)
        {
            _monthClose = monthClose;
        }

        [HttpPost]
        [Route("/monthClose")]
        public IActionResult CloseMonth([FromBody] MonthResumeApi resumeApi) 
        {
            MonthIndexApi index = MonthIndexMapper.MapToApi( 
                    _monthClose.CloseMonth( 
                        ExpencesMapper.MapToModel( resumeApi.ExpencesApi ),
                        IncomeMapper.MapToModel( resumeApi.IncomesApi ),
                        AmountMapper.MapToModel( resumeApi.AmountApi ) 
                    ) 
                );
            return Ok(index);
        }
    }
}
