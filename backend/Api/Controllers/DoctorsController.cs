using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService ?? throw new ArgumentNullException(nameof(doctorService));
        }

        /// <summary>
        /// Retrieves all doctors.
        /// </summary>
        /// <returns>A list of doctors.</returns>
        /// <response code="200">Returns the list of doctors.</response>
        /// <response code="500">If an error occurred while retrieving doctors.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        /// <summary>
        /// Retrieves a doctor by ID.
        /// </summary>
        /// <param name="id">The doctor ID.</param>
        /// <returns>The doctor details.</returns>
        /// <response code="200">Returns the doctor details.</response>
        /// <response code="404">If the doctor is not found.</response>
        /// <response code="500">If an error occurred while retrieving the doctor.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctorById(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                throw new NotFoundException("Doctor not found.");
            }

            return Ok(doctor);
        }

        /// <summary>
        /// Adds a new doctor.
        /// </summary>
        /// <param name="doctorDto">The doctor details.</param>
        /// <returns>The created doctor.</returns>
        /// <response code="201">Returns the created doctor.</response>
        /// <response code="400">If the doctor details are invalid.</response>
        /// <response code="500">If an error occurred while adding the doctor.</response>
        [HttpPost]
        public async Task<ActionResult<DoctorDto>> AddDoctor([FromBody] DoctorDto doctorDto)
        {
            await _doctorService.AddDoctorAsync(doctorDto);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctorDto.Id }, doctorDto);
        }

        /// <summary>
        /// Updates an existing doctor.
        /// </summary>
        /// <param name="id">The doctor ID.</param>
        /// <param name="doctorDto">The updated doctor details.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the doctor was successfully updated.</response>
        /// <response code="400">If the doctor ID does not match.</response>
        /// <response code="404">If the doctor is not found.</response>
        /// <response code="500">If an error occurred while updating the doctor.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorDto>> UpdateDoctor(int id, [FromBody] DoctorDto doctorDto)
        {
            if (id != doctorDto.Id)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Id", new[] { "Doctor ID mismatch." } }
                });
            }

            await _doctorService.UpdateDoctorAsync(doctorDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a doctor by ID.
        /// </summary>
        /// <param name="id">The doctor ID.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the doctor was successfully deleted.</response>
        /// <response code="404">If the doctor is not found.</response>
        /// <response code="500">If an error occurred while deleting the doctor.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DoctorDto>> DeleteDoctor(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return NoContent();
        }
    }
}
