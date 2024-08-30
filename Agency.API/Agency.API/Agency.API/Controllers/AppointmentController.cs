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
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // POST: api/Appointment/SetAppointment
        [HttpPost("SetAppointment")]
        public async Task<ActionResult<AppointmentDto>> SetAppointment(AppointmentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var appointment = await _appointmentService.SetAppointment(model);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
