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

        /// <summary>
        /// Books a new appointment.
        /// </summary>
        /// <param name="bookAppointmentDto">The appointment details.</param>
        /// <returns>The created appointment.</returns>
        /// <response code="201">Returns the created appointment.</response>
        /// <response code="400">If the appointment is null or invalid.</response>
        /// <response code="500">If an error occurred while booking the appointment.</response>
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

        /// <summary>
        /// Retrieves an appointment by ID.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        /// <returns>The appointment details.</returns>
        /// <response code="200">Returns the appointment details.</response>
        /// <response code="404">If the appointment is not found.</response>
        /// <response code="500">If an error occurred while retrieving the appointment.</response>
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

        /// <summary>
        /// Retrieves all appointments for a specific patient.
        /// </summary>
        /// <param name="patientId">The patient ID.</param>
        /// <returns>A list of appointments for the patient.</returns>
        /// <response code="200">Returns the list of appointments.</response>
        /// <response code="401">If the user is not authorized.</response>
        /// <response code="500">If an error occurred while retrieving the appointments.</response>
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

        /// <summary>
        /// Deletes an appointment by ID.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the appointment was successfully deleted.</response>
        /// <response code="401">If the user is not authorized.</response>
        /// <response code="500">If an error occurred while deleting the appointment.</response>
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
