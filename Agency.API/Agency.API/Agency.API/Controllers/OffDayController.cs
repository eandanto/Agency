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

        [HttpGet("SetOffDay")]
        public async Task<ActionResult<bool>> SetOffDay(DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _offDayService.SetOffDay(date);
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

        // GET: api/OffDay/GetOffDays
        [HttpGet("GetOffDays")]
        public async Task<ActionResult<List<OffDayDto>>> GetOffDays()
        {
            try
            {
                var offDays = await _offDayService.GetOffDays();
                return Ok(offDays);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/OffDay/RemoveOffDay
        [HttpGet("RemoveOffDay")]
        public async Task<ActionResult<bool>> RemoveOffDay(DateTime date)
        {
            try
            {
                var result = await _offDayService.RemoveOffDay(date);
                if (!result)
                {
                    return NotFound($"{date} is not an off day.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
