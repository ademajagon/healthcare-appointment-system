using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepostiory)
        {
            _patientRepository = patientRepostiory ?? throw new ArgumentNullException(nameof(patientRepostiory));

        }

        public Task AddPatientAsync(PatientDto patientDto)
        {
            var patient = new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                DateOfBirth = patientDto.DateOfBirth,
                Gender = patientDto.Gender,
                Address = patientDto.Address,
                PhoneNumber = patientDto.PhoneNumber,
                Email = patientDto.Email
            };

            return _patientRepository.AddAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _patientRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _patientRepository.GetAllAsync();

            return patients.Select(p => new PatientDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                Address = p.Address,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email
            });
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                return null;
            }

            return new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email
             };
        }

        public async Task UpdatePatientAsync(PatientDto patientDto)
        {
            var patient = await _patientRepository.GetByIdAsync(patientDto.Id);
            
            if (patient == null)
            {
                throw new Exception("Patient not found");
            }

            patient.FirstName = patientDto.FirstName;
            patient.LastName = patientDto.LastName;
            patient.DateOfBirth = patientDto.DateOfBirth;
            patient.Gender = patientDto.Gender;
            patient.Address = patientDto.Address;
            patient.PhoneNumber = patientDto.PhoneNumber;
            patient.Email = patientDto.Email;

            await _patientRepository.UpdateAsync(patient);
        }
    }
}
