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
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository
            )
        {
            _patientRepository = patientRepository;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<BookAppointmentDto> BookAppointmentAsync(BookAppointmentDto appointmentDto)
        {
            var appointment = new Appointment
            {
                DoctorId = appointmentDto.DoctorId,
                PatientId = appointmentDto.PatientId,
                AppointmentDate = appointmentDto.AppointmentDate,
                AppointmentTime = appointmentDto.AppointmentTime,
                Notes = appointmentDto.Notes
            };

            var addedAppointment = await _appointmentRepository.AddAsync(appointment);

            return new BookAppointmentDto
            {
                DoctorId = addedAppointment.DoctorId,
                PatientId = addedAppointment.PatientId,
                AppointmentDate = addedAppointment.AppointmentDate,
                AppointmentTime = addedAppointment.AppointmentTime,
                Notes = addedAppointment.Notes
            };
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }

            var doctor = await _doctorRepository.GetByIdAsync(appointment.DoctorId);
            var patient = await _patientRepository.GetByIdAsync(appointment.PatientId);

            return new AppointmentDto
            {
                Id = appointment.Id,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Notes = appointment.Notes,
                Doctor = new DoctorDto
                {
                    Id = appointment.Doctor.Id,
                    FirstName = appointment.Doctor.FirstName,
                    LastName = appointment.Doctor.LastName,
                    Specialization = appointment.Doctor.Specialization,
                    Biography = appointment.Doctor.Biography,
                    IsAvailable = appointment.Doctor.IsAvailable
                },
                Patient = new PatientDto
                {
                    Id = appointment.Patient.Id,
                    FirstName = appointment.Patient.FirstName,
                    LastName = appointment.Patient.LastName,
                    DateOfBirth = appointment.Patient.DateOfBirth,
                    Gender = appointment.Patient.Gender,
                    Address = appointment.Patient.Address,
                    PhoneNumber = appointment.Patient.PhoneNumber,
                    Email = appointment.Patient.Email
                }
            };
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            var appointments = await _appointmentRepository.GetByPatientIdAsync(patientId);
            var appointmentDtos = new List<AppointmentDto>();

            foreach (var appointment in appointments)
            {
                var doctor = await _doctorRepository.GetByIdAsync(appointment.DoctorId);
                var patient = await _patientRepository.GetByIdAsync(appointment.PatientId);

                appointmentDtos.Add(new AppointmentDto
                {
                    Id = appointment.Id,
                    DoctorId = appointment.DoctorId,
                    PatientId = appointment.PatientId,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime,
                    Notes = appointment.Notes,
                    Doctor = new DoctorDto
                    {
                        Id = doctor.Id,
                        FirstName = doctor.FirstName,
                        LastName = doctor.LastName,
                        Specialization = doctor.Specialization,
                        Biography = doctor.Biography,
                        IsAvailable = doctor.IsAvailable
                    },
                    Patient = new PatientDto
                    {
                        Id = patient.Id,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        DateOfBirth = patient.DateOfBirth,
                        Gender = patient.Gender,
                        Address = patient.Address,
                        PhoneNumber = patient.PhoneNumber,
                        Email = patient.Email
                    }
                });
            }

            return appointmentDtos;
        }
    }
}
