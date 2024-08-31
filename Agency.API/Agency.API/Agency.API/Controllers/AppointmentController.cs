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

        // GET: api/Appointment/GetMyAppointments?id={id}pageNo=1&pageSize=10
        [HttpGet("GetMyAppointments")]
        public async Task<ActionResult<AppointmentListDto>> GetMyAppointments(Guid id, int pageNo = 0, int pageSize = 10)
        {
            try
            {
                var appointments = await _appointmentService.GetMyAppointments(id, pageNo, pageSize);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Appointment/GetAllAppointments?pageNo=1&pageSize=10&date=2024-09-01
        [HttpGet("GetAllAppointments")]
        public async Task<ActionResult<AppointmentListDto>> GetAllAppointments(int pageNo, int pageSize, DateTime date)
        {
            try
            {
                date = date.Date;
                var appointments = await _appointmentService.GetAllAppointments(pageNo, pageSize, date);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
