using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Server.Mapper;
using MadExpenceTracker.Server.Model;
using Microsoft.AspNetCore.Mvc;

namespace MadExpenceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _service;

        public ConfigurationController(IConfigurationService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/configuration")]
        public IActionResult GetConfiguration()
        {
            return Ok(ConfigurationMapper.MapToApi(_service.GetConfiguration()));
        }

        [HttpPost]
        [Route("/configuration")]
        public IActionResult SaveConfiguration([FromBody] ConfigurationApi configuration)
        {
            return Ok(ConfigurationMapper.MapToApi( 
                _service.SetConfiguration(
                    ConfigurationMapper.MapToModel(configuration)
                    )
                ));
        }

        [HttpPut]
        [Route("/configuration")]
        public IActionResult UpdateConfiguration([FromBody] ConfigurationApi configuration)
        {
            return Ok(ConfigurationMapper.MapToApi(
                _service.UpdateConfiguration(
                    ConfigurationMapper.MapToModel(configuration)
                    )
                ));
        }
    }
}
