using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Doctor entity)
        {
            await _dbContext.Doctors.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _dbContext.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _dbContext.Doctors.Remove(doctor);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _dbContext.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateAsync(Doctor entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
