using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define roles
            var rolePatientId = Guid.NewGuid().ToString();
            var roleDoctorId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = rolePatientId,
                    Name = "Patient",
                    NormalizedName = "PATIENT"
                },
                new IdentityRole
                {
                    Id = roleDoctorId,
                    Name = "Doctor",
                    NormalizedName = "DOCTOR"
                }
            );

            // Define users
            var userPatientId = Guid.NewGuid().ToString();
            var userDoctorId = Guid.NewGuid().ToString();
            var userPatientId2 = Guid.NewGuid().ToString(); // New patient user

            var hasher = new PasswordHasher<ApplicationUser>();

            var patientUser = new ApplicationUser
            {
                Id = userPatientId,
                UserName = "patient@example.com",
                NormalizedUserName = "PATIENT@EXAMPLE.COM",
                Email = "patient@example.com",
                NormalizedEmail = "PATIENT@EXAMPLE.COM",
                FirstName = "PatientFirstName",
                LastName = "PatientLastName",
                Gender = "Female",
                DateOfBirth = new DateTime(1990, 2, 2),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Patient@123"),
            };

            var doctorUser = new ApplicationUser
            {
                Id = userDoctorId,
                UserName = "doctor@example.com",
                NormalizedUserName = "DOCTOR@EXAMPLE.COM",
                Email = "doctor@example.com",
                NormalizedEmail = "DOCTOR@EXAMPLE.COM",
                FirstName = "DoctorFirstName",
                LastName = "DoctorLastName",
                Gender = "Male",
                DateOfBirth = new DateTime(1980, 1, 1),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Doctor@123")
            };

            var patientUser2 = new ApplicationUser
            {
                Id = userPatientId2,
                UserName = "patient2@example.com",
                NormalizedUserName = "PATIENT2@EXAMPLE.COM",
                Email = "patient2@example.com",
                NormalizedEmail = "PATIENT2@EXAMPLE.COM",
                FirstName = "SecondPatientFirstName",
                LastName = "SecondPatientLastName",
                Gender = "Male",
                DateOfBirth = new DateTime(1995, 3, 3),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Patient2@123"),
            };

            modelBuilder.Entity<ApplicationUser>().HasData(patientUser, doctorUser, patientUser2);

            // Assign roles to users
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = rolePatientId,
                    UserId = userPatientId
                },
                new IdentityUserRole<string>
                {
                    RoleId = roleDoctorId,
                    UserId = userDoctorId
                },
                new IdentityUserRole<string>
                {
                    RoleId = rolePatientId,
                    UserId = userPatientId2 // Assign role to new patient user
                }
            );

            // Seed Doctors and Patients entities without ApplicationUserId
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Specialization = "General Practitioner",
                    Biography = "Dr. John Doe is a General Practitioner with 10 years of experience.",
                    ImageUrl = "https://images.unsplash.com/photo-1622253692010-333f2da6031d?q=80&w=2564&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    IsAvailable = true
                },
                new Doctor
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Specialization = "Pediatrician",
                    Biography = "Dr. Jane Smith is a Pediatrician with 8 years of experience.",
                    ImageUrl = "https://images.unsplash.com/photo-1594824476967-48c8b964273f?q=80&w=2574&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    IsAvailable = true
                }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 11,
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1990, 2, 2),
                    Gender = "Female",
                    Address = "456 Oak St",
                    PhoneNumber = "987-654-3210",
                    Email = "patient@example.com", // Same email as the patient user
                    UserId = userPatientId
                },
                new Patient
                {
                    Id = 12, // New patient
                    FirstName = "SecondPatientFirstName",
                    LastName = "SecondPatientLastName",
                    DateOfBirth = new DateTime(1995, 3, 3),
                    Gender = "Male",
                    Address = "789 Pine St",
                    PhoneNumber = "123-123-1234",
                    Email = "patient2@example.com", // Same email as the new patient user
                    UserId = userPatientId2
                }
            );
        }


    }
}
