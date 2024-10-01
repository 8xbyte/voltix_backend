using Microsoft.AspNetCore.Mvc; 
using Voltix.Auth.Models;
using Voltix.Auth.Services;

namespace Voltix.Auth.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(IAuthService authService, IUserService userService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IUserService _userService = userService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
        {
            var token = await _authService.RegisterAsync(model.Username, model.Email, model.Password);
            if (token == null)
            {
                return BadRequest(new
                {
                    Message = "User is already registered"
                });
            }

            return Ok(new { Token = token });

            // try
            // {
            //     return Ok(new
            //     {
            //         Token = await _authService.RegisterAsync(model.Username, model.Email, model.Password)
            //     });
            // }
            // catch (ArgumentException ex)
            // {
            //     if (ex.Message.Contains("Username is alreade taken"))
            //     {
            //         return Conflict($"Username '{model.Username}' is already taken");
            //     }
            //     else if (ex.Message.Contains("Email is alreade taken"))
            //     {
            //         return Conflict($"Email '{model.Email}' is already taken");
            //     }
            //     else
            //     {
            //         return BadRequest(ex.Message);
            //     }
            // }
            // catch (Exception ex)
            // {
            //     return StatusCode(500, "An error occured while registering the user");
            // }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var token = await _authService.LoginAsync(model.Email, model.Password);
            if (token == null)
            {
                return BadRequest(new
                {
                    Message = "Incorrect username or password"
                });
            }

            return Ok(new { Token = token });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest model)
        {
            var result = await _userService.DeleteUserByUsernameAsync(model.Username);
            if(!result) return NotFound(new { Message = "User not found" });
            return Ok(new { Message = "User has been deleted" });
        }
        
    }
}