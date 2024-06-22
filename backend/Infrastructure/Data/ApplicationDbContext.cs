using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
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
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1980, 1, 1),
                    Gender = "Male",
                    Address = "123 Main St",
                    PhoneNumber = "123-456-7890",
                    Email = "john.doe@example.com"
                },
                new Patient
                {
                    Id = 12,
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1990, 2, 2),
                    Gender = "Female",
                    Address = "456 Oak St",
                    PhoneNumber = "987-654-3210",
                    Email = "jane.smith@example.com"
                }
            );
        }
    }
}
