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

        // POST: api/Configuration/SetConfiguration
        [HttpPost("SetConfiguration")]
        public async Task<ActionResult<bool>> SetConfiguration(ConfigurationDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _configurationService.SetConfiguration(model);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new { message = "Failed to set configuration." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
