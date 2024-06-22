using Application.DTOs.Identity;
using Application.Interfaces;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<IdentityResult> AddRoleAsync(ApplicationUser user, string role)
        {
            if (!await _authRepository.RoleExistsAsync(role))
            {
                await _authRepository.CreateRoleAsync(new IdentityRole(role));
            }

            return await _authRepository.AddToRoleAsync(user, role);
        }
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _authRepository.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth
            };

            return await _authRepository.CreateUserAsync(user, model.Password);
        }

        public async Task<bool> RoleExistsAsync(string role)
        {
            return await _authRepository.RoleExistsAsync(role);
        }
    }
}
