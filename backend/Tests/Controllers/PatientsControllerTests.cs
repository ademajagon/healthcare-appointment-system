using Api.Controllers;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class PatientsControllerTests
    {
        private readonly Mock<IPatientService> _patientServiceMock;
        private readonly PatientsController _controller;

        public PatientsControllerTests()
        {
            _patientServiceMock = new Mock<IPatientService>();
            _controller = new PatientsController(_patientServiceMock.Object);
        }

        [Fact]
        public async Task GetAllPatients_ReturnsOkResult_WithListOfPatients()
        {
            var patients = new List<PatientDto>
            {
                new PatientDto { Id = 1, FirstName = "John", LastName = "Doe" },
                new PatientDto { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };
            _patientServiceMock.Setup(service => service.GetAllPatientsAsync()).ReturnsAsync(patients);

            var result = await _controller.GetAllPatients();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<PatientDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetPatientById_ReturnsOkResult_WithPatient()
        {
            var patientId = 1;
            var patient = new PatientDto
            {
                Id = patientId,
                FirstName = "John",
                LastName = "Doe"
            };

            _patientServiceMock.Setup(service => service.GetPatientByIdAsync(patientId)).ReturnsAsync(patient);

            var result = await _controller.GetPatientById(patientId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(patientId, returnValue.Id);
        }

        [Fact]
        public async Task AddPatient_ReturnsCreatedResult_WithPatient()
        {
            var patientDto = new PatientDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };

            _patientServiceMock.Setup(service => service.AddPatientAsync(patientDto)).Returns(Task.CompletedTask);
            var result = await _controller.AddPatients(patientDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<PatientDto>(createdResult.Value);
            Assert.Equal(patientDto.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetMyDetails_ReturnsOkResult_WithPatient()
        {
            var userId = "123";
            var patient = new PatientDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                UserId = userId
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _patientServiceMock.Setup(service => service.GetPatientByUserIdAsync(userId)).ReturnsAsync(patient);
            var result = await _controller.GetMyDetails();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(userId, returnValue.UserId);
        }
    }
}
