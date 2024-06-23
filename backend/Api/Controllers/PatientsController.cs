using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients()
        {
            try
            {
                var patients = await _patientService.GetAllPatientsAsync();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                throw new CustomApplicationException($"An error occurred while retrieving patients: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatientById(int id)
        {
            try
            {
                var patient = await _patientService.GetPatientByIdAsync(id);
                if (patient == null)
                {
                    throw new NotFoundException("Patient not found.");
                }

                return Ok(patient);
            }
            catch (Exception ex)
            {
                throw new CustomApplicationException($"An error occurred while retrieving the patient: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> AddPatients([FromBody] PatientDto patientDto)
        {
            try
            {
                await _patientService.AddPatientAsync(patientDto);
                return CreatedAtAction(nameof(GetPatientById), new { id = patientDto.Id }, patientDto);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                throw new CustomApplicationException($"An error occurred while adding the patient: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> UpdatePatient(int id, [FromBody] PatientDto patientDto)
        {
            try
            {
                if (id != patientDto.Id)
                {
                    throw new ValidationException(new Dictionary<string, string[]>
                    {
                        { "Id", new[] { "Patient ID mismatch." } }
                    });
                }

                await _patientService.UpdatePatientAsync(patientDto);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                throw new CustomApplicationException($"An error occurred while updating the patient: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientDto>> DeletePatient(int id)
        {
            try
            {
                await _patientService.DeletePatientAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new CustomApplicationException($"An error occurred while deleting the patient: {ex.Message}");
            }
        }

        [HttpGet("me"), Authorize]
        public async Task<ActionResult<PatientDto>> GetMyDetails()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return Unauthorized();
                }

                var patient = await _patientService.GetPatientByUserIdAsync(userId);
                if (patient == null)
                {
                    throw new NotFoundException("Patient details not found.");
                }

                return Ok(patient);
            }
            catch (Exception ex)
            {
                throw new CustomApplicationException($"An error occurred while retrieving your details: {ex.Message}");
            }
        }
    }
}
