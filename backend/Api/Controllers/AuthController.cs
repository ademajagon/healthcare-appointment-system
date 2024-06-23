using Application.DTOs.Identity;
using Application.Interfaces;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The registration details.</param>
        /// <returns>Confirmation message on successful registration.</returns>
        /// <response code="200">If the user is registered successfully.</response>
        /// <response code="400">If the registration details are invalid.</response>
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (result.Succeeded)
            {
                var user = await _authService.GetUserByEmailAsync(model.Email);

                var role = "Patient";
                var roleResult = await _authService.AddRoleAsync(user, role);

                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _authService.CreatePatientAsync(user, model);

                return Ok(new { message = "User registered successfully!" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }
    }
}
