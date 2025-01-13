using Microsoft.AspNetCore.Mvc;
using RealTimeNotificationSys.Core.Dto;
using RealTimeNotificationSys.Core.Services;
using RealTimeNotificationSys.Core.Interfaces;
using RealTimeNotificationSys.Core.Entities;
using System.Threading.Tasks;

namespace RealTimeNotificationSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(registerDto.Name, registerDto.Email, registerDto.Password);
                return Ok(user);  // Return user details or any other response as needed
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
