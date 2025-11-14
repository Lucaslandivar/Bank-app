using _7Bank.Api.DTOs;
using _7Bank.Api.Models;
using _7Bank.Api.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace _7Bank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Users user)
        {
            string createdByRole = "Gerente";

            var createdUser = await _userService.CreateUserAsync(user, createdByRole);

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var user = await _userService.ValidateLoginAsync(loginDto.Email, loginDto.Password);

            if (user == null)
                return Unauthorized("Email ou senha inválid");

            return Ok(user);
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            var user = await _userService.GetByCpfAsync(cpf);
            if(user==null) return NotFound();
            if (string.IsNullOrEmpty(cpf)) return BadRequest();
            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            if (user==null) return NotFound();
            return Ok(user);
        }
    }
}
