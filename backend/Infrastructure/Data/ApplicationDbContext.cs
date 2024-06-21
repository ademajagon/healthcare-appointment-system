using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Specialization = "General Practitioner",
                    Biography = "Dr. John Doe is a General Practitioner with 10 years of experience.",
                    IsAvailable = true
                },
                new Doctor
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Specialization = "Pediatrician",
                    Biography = "Dr. Jane Smith is a Pediatrician with 8 years of experience.",
                    IsAvailable = true
                }
            );
        }
    }
}
