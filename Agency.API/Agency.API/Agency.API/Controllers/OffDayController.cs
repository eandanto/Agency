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
    public class OffDayController : ControllerBase
    {
        private readonly IOffDayService _offDayService;

        public OffDayController(IOffDayService offDayService)
        {
            _offDayService = offDayService;
        }

        // POST: api/OffDay/SetOffDay
        [HttpPost("SetOffDay")]
        public async Task<ActionResult<bool>> SetOffDay(OffDayDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _offDayService.SetOffDay(model);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new { message = "Failed to set off day." });
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
