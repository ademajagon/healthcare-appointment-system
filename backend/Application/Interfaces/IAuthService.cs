using Application.DTOs.Identity;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto model);
        Task<IdentityResult> AddRoleAsync(ApplicationUser user, string role);
        Task<bool> RoleExistsAsync(string role);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
    }
}
