using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace Ecommerce.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;


        public AuthController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpPost("log")]
        public async Task<IActionResult> loggg(string message)
        {
            return Ok(message);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                Log.Information("The user entered wrong data");
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userService.RegisterAsync(
                    registerDto.FirstName,
                    registerDto.LastName,
                    registerDto.Email,
                    registerDto.Password);

                return Ok("User registered successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.AuthenticateAsync(loginDto.Email, loginDto.Password);
            if (user[0] == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var token = _userService.GenerateJwtTokenAsync((User)user[0]);
            return Ok(new { token = token.Result, user = (UserDto)user[1] });
        }
    }
}
