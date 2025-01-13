using Microsoft.AspNetCore.Mvc;
using RealTimeNotificationSys.Core.Dto;
using RealTimeNotificationSys.Core.Services;
using RealTimeNotificationSys.Core.Interfaces;

namespace RealTimeNotificationSys.Controllers
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



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Ensure email and password are provided
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest(new { message = "Email and password are required." });
            }

            // Authenticate the user and generate JWT token
            var token = await _authService.AuthenticateAsync(loginDto.Email, loginDto.Password);

            if (token == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            // Return the JWT token if authentication is successful
            return Ok(new { token });
        }
    }
}
