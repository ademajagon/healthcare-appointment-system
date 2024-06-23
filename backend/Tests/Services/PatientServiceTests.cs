using Application.DTOs;
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
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly IPatientService _patientService;

        public PatientServiceTests()
        {
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _patientService = new PatientService(_patientRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllPatientsAsync_ReturnsPatients()
        {
            var patients = new List<Patient>
            {
                new Patient { Id = 1, FirstName = "John", LastName = "Doe" },
                new Patient { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            _patientRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);

            var result = await _patientService.GetAllPatientsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetPatientByIdAsync_ReturnsPatient()
        {
            var patientId = 1;
            var patient = new Patient
            {
                Id = patientId,
                FirstName = "John",
                LastName = "Doe"
            };

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patientId)).ReturnsAsync(patient);

            var result = await _patientService.GetPatientByIdAsync(patientId);

            Assert.NotNull(result);
            Assert.Equal(patientId, result.Id);
        }

        [Fact]
        public async Task AddPatientAsync_AddsPatient()
        {
            var patientDto = new PatientDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };

            var patient = new Patient
            {
                Id = patientDto.Id,
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName
            };

            _patientRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Patient>())).Returns(Task.CompletedTask);

            await _patientService.AddPatientAsync(patientDto);

            _patientRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Patient>()), Times.Once);
        }

        [Fact]
        public async Task UpdatePatientAsync_UpdatesPatient()
        {
            var patientDto = new PatientDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };

            var patient = new Patient
            {
                Id = patientDto.Id,
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName
            };

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patientDto.Id)).ReturnsAsync(patient);
            _patientRepositoryMock.Setup(repo => repo.UpdateAsync(patient)).Returns(Task.CompletedTask);

            await _patientService.UpdatePatientAsync(patientDto);

            _patientRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Patient>()), Times.Once);
        }

        [Fact]
        public async Task DeletePatientAsync_DeletesPatient()
        {
            var patientId = 1;

            _patientRepositoryMock.Setup(repo => repo.DeleteAsync(patientId)).Returns(Task.CompletedTask);

            await _patientService.DeletePatientAsync(patientId);

            _patientRepositoryMock.Verify(repo => repo.DeleteAsync(patientId), Times.Once);
        }
    }
}
