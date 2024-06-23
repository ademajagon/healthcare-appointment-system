using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<ActionResult<BookAppointmentDto>> BookAppointment([FromBody] BookAppointmentDto bookAppointmentDto)
        {
            try
            {
                var appointment = await _appointmentService.BookAppointmentAsync(bookAppointmentDto);
                return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
            }
            catch (Exception ex)
            {
                throw new CustomApplicationException($"An error occurred while booking the appointment: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointmentById(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);

                if (appointment == null)
                {
                    throw new NotFoundException("Appointment not found.");
                }

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                throw new CustomApplicationException($"An error occurred while retrieving the appointment: {ex.Message}");
            }
        }

        [HttpGet("patient/{patientId}"), Authorize]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByPatientId(int patientId)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByPatientIdAsync(patientId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                throw new CustomApplicationException($"An error occurred while retrieving appointments for patient {patientId}: {ex.Message}");
            }
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                await _appointmentService.DeleteAppointmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new CustomApplicationException($"An error occurred while deleting the appointment: {ex.Message}");
            }
        }
    }
}
