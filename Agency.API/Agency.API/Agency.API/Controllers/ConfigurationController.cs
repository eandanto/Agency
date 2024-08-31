using Agency.Application.DTOs;
using Agency.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Agency.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpGet("getConfigurations")]
        public async Task<ActionResult<List<ConfigurationDto>>> GetConfigurations()
        {
            try
            {
                var configs = await _configurationService.GetAllConfigurations();
                return Ok(configs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("updateConfiguration")]
        public async Task<ActionResult> UpdateConfiguration([FromBody] List<ConfigurationDto> configs)
        {
            try
            {
                if (configs == null)
                    throw new Exception("Nothing to update");

                for (int i = 0; i < configs.Count; i++)
                {
                    await _configurationService.UpdateConfiguration(configs[i]);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
