using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElectronicsStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Invalid client request");

            var user = _unitOfWork.EmployeeRepository.GetbyUserName(request.Username);
            if (user == null)
            {
                return Unauthorized(new LoginResponseDTO { Success = false, Message = "Invalid username or password" });
            }

            // Verify password strictly using BCrypt
            bool isPasswordValid = false;
            try
            {
                if (!string.IsNullOrEmpty(user.Password))
                {
                    isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
                }
            }
            catch
            {
                isPasswordValid = false;
            }

            if (!isPasswordValid)
            {
                return Unauthorized(new LoginResponseDTO { Success = false, Message = "Invalid username or password" });
            }

            var token = GenerateJwtToken(user);

            return Ok(new LoginResponseDTO
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                UserId = user.ID,
                Username = user.UserName,
                FullName = user.FullName,
                Roles = user.Role
            });
        }

        private string GenerateJwtToken(Employees user)
        {
            var jwtSecret = _configuration["Jwt:Key"] ?? "super_secret_key_electronic_store_12345!@#";
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role ? "Admin" : "Staff")
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1), // Token valid for 1 day
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
