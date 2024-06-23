using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class DoctorServiceTests
    {
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
        private readonly IDoctorService _doctorService;

        public DoctorServiceTests()
        {
            _doctorRepositoryMock = new Mock<IDoctorRepository>();
            _doctorService = new DoctorService(_doctorRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllDoctorsAsync_ReturnsDoctorList()
        {
            var doctors = new List<Doctor>
            {
                new Doctor { Id = 1, FirstName = "John", LastName = "Doe", Specialization = "Cardiology" },
                new Doctor { Id = 2, FirstName = "Jane", LastName = "Smith", Specialization = "Neurology" }
            };
            _doctorRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(doctors);

            var result = await _doctorService.GetAllDoctorsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
