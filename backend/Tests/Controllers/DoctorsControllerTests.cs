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
    public class DoctorsControllerTests
    {
        private readonly Mock<IDoctorService> _doctorServiceMock;
        private readonly DoctorsController _controller;

        public DoctorsControllerTests()
        {
            _doctorServiceMock = new Mock<IDoctorService>();
            _controller = new DoctorsController(_doctorServiceMock.Object);
        }

        [Fact]
        public async Task GetAllDoctors_ReturnsOkResult_WithListOfDoctors()
        {
            var doctors = new List<DoctorDto>
            {
                new DoctorDto { Id = 1, FirstName = "John", LastName = "Doe" },
                new DoctorDto { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };
            _doctorServiceMock.Setup(service => service.GetAllDoctorsAsync()).ReturnsAsync(doctors);

            var result = await _controller.GetAllDoctors();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<DoctorDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

    }
}
