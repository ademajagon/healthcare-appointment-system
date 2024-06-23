using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Tests.Services
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly IAppointmentService _appointmentService;

        public AppointmentServiceTests()
        {
            _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            _doctorRepositoryMock = new Mock<IDoctorRepository>();
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _appointmentService = new AppointmentService(
                _appointmentRepositoryMock.Object,
                _doctorRepositoryMock.Object,
                _patientRepositoryMock.Object
            );
        }

        [Fact]
        public async Task BookAppointmentAsync_AddsAppointment()
        {
            var appointmentDto = new BookAppointmentDto
            {
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Now,
                AppointmentTime = DateTime.Now,
                Notes = "Test Notes"
            };

            var appointment = new Appointment
            {
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = appointmentDto.AppointmentDate,
                AppointmentTime = appointmentDto.AppointmentTime,
                Notes = appointmentDto.Notes
            };

            _appointmentRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Appointment>()))
                                      .ReturnsAsync(appointment);

            var result = await _appointmentService.BookAppointmentAsync(appointmentDto);

            Assert.Equal(appointment.DoctorId, result.DoctorId);
            Assert.Equal(appointment.PatientId, result.PatientId);
        }

        [Fact]
        public async Task DeleteAppointmentAsync_DeletesAppointment()
        {
            var appointmentId = 1;

            _appointmentRepositoryMock.Setup(repo => repo.DeleteAsync(appointmentId))
                                      .Returns(Task.CompletedTask);

            await _appointmentService.DeleteAppointmentAsync(appointmentId);

            _appointmentRepositoryMock.Verify(repo => repo.DeleteAsync(appointmentId), Times.Once);
        }
    }
}
