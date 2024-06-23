using Api.Controllers;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class AppointmentsControllerTests
    {
        private readonly Mock<IAppointmentService> _appointmentServiceMock;
        private readonly AppointmentsController _controller;

        public AppointmentsControllerTests()
        {
            _appointmentServiceMock = new Mock<IAppointmentService>();
            _controller = new AppointmentsController(_appointmentServiceMock.Object);
        }

        [Fact]
        public async Task BookAppointment_ReturnsCreatedResult_WithAppointment()
        {
            var appointmentDto = new BookAppointmentDto
            {
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Now,
                AppointmentTime = DateTime.Now,
                Notes = "Test Notes"
            };
            var createdAppointment = new BookAppointmentDto
            {
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Now,
                AppointmentTime = DateTime.Now,
                Notes = "Test Notes"
            };

            _appointmentServiceMock.Setup(service => service.BookAppointmentAsync(appointmentDto))
                                   .ReturnsAsync(createdAppointment);

            var result = await _controller.BookAppointment(appointmentDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<BookAppointmentDto>(createdResult.Value);
            Assert.Equal(appointmentDto.DoctorId, returnValue.DoctorId);
            Assert.Equal(appointmentDto.PatientId, returnValue.PatientId);
        }

        [Fact]
        public async Task GetAppointmentById_ReturnsOkResult_WithAppointment()
        {
            var appointmentId = 1;
            var appointment = new AppointmentDto
            {
                Id = appointmentId,
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Now,
                AppointmentTime = DateTime.Now,
                Notes = "Test Notes"
            };

            _appointmentServiceMock.Setup(service => service.GetAppointmentByIdAsync(appointmentId))
                                   .ReturnsAsync(appointment);

            var result = await _controller.GetAppointmentById(appointmentId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<AppointmentDto>(okResult.Value);
            Assert.Equal(appointmentId, returnValue.Id);
        }

        [Fact]
        public async Task GetAppointmentsByPatientId_ReturnsOkResult_WithAppointments()
        {
            var patientId = 1;
            var appointments = new List<AppointmentDto>
            {
                new AppointmentDto { Id = 1, DoctorId = 1, PatientId = patientId, AppointmentDate = DateTime.Now, AppointmentTime = DateTime.Now, Notes = "Test Notes" },
                new AppointmentDto { Id = 2, DoctorId = 2, PatientId = patientId, AppointmentDate = DateTime.Now, AppointmentTime = DateTime.Now, Notes = "Test Notes" }
            };

            _appointmentServiceMock.Setup(service => service.GetAppointmentsByPatientIdAsync(patientId))
                                   .ReturnsAsync(appointments);

            var result = await _controller.GetAppointmentsByPatientId(patientId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<AppointmentDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task DeleteAppointment_ReturnsNoContent()
        {
            var appointmentId = 1;

            _appointmentServiceMock.Setup(service => service.DeleteAppointmentAsync(appointmentId))
                                   .Returns(Task.CompletedTask);

            var result = await _controller.DeleteAppointment(appointmentId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
