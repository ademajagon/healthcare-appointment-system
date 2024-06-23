using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient> GetByUserIdAsync(string userId);
    }
}
