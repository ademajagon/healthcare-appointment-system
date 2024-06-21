using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto> GetDoctorByIdAsync(int id);
        Task AddDoctorAsync(DoctorDto doctorDto);
        Task UpdateDoctorAsync(DoctorDto doctorDto);
        Task DeleteDoctorAsync(int id);
    }
}
