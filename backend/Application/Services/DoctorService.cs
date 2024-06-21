using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        }

        public Task AddDoctorAsync(DoctorDto doctorDto)
        {
            var doctor = new Doctor
            {
                FirstName = doctorDto.FirstName,
                LastName = doctorDto.LastName,
                Specialization = doctorDto.Specialization,
                Biography = doctorDto.Biography,
                IsAvailable = doctorDto.IsAvailable
            };

            return _doctorRepository.AddAsync(doctor);
        }

        public async Task DeleteDoctorAsync(int id)
        {
            await _doctorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync();

            // Mapping to DTOs can be done here or using a library like AutoMapper

            return doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Specialization = d.Specialization,
                Biography = d.Biography,
                IsAvailable = d.IsAvailable
            });
        }

        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null)
            {
                return null;
            }

            return new DoctorDto
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                Biography = doctor.Biography,
                IsAvailable = doctor.IsAvailable
            };
        }

        public async Task UpdateDoctorAsync(DoctorDto doctorDto)
        {
            var doctor = await _doctorRepository.GetByIdAsync(doctorDto.Id);
            if (doctor == null) return;

            doctor.FirstName = doctorDto.FirstName;
            doctor.LastName = doctorDto.LastName;
            doctor.Specialization = doctorDto.Specialization;
            doctor.Biography = doctorDto.Biography;
            doctor.IsAvailable = doctorDto.IsAvailable;

            await _doctorRepository.UpdateAsync(doctor);
        }
    }
}
