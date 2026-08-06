using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequestDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return BadRequest(new ServerResponse<bool>(false, "Invalid password change request."));
            }

            var employee = _unitOfWork.EmployeeRepository.GetById(request.EmployeeId);
            if (employee == null)
            {
                return NotFound(new ServerResponse<bool>(false, "User not found."));
            }

            // Verify old password
            bool isOldPasswordValid = false;
            try
            {
                if (!string.IsNullOrEmpty(employee.Password))
                {
                    isOldPasswordValid = BCrypt.Net.BCrypt.Verify(request.OldPassword, employee.Password);
                }
            }
            catch
            {
                isOldPasswordValid = false;
            }

            if (!isOldPasswordValid)
            {
                return BadRequest(new ServerResponse<bool>(false, "Incorrect old password."));
            }

            // Hash and update new password
            employee.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.SaveChanges();

            return Ok(new ServerResponse<bool>(true, "Password changed successfully."));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterRequestDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new ServerResponse<bool>(false, "Username and Password are required."));
            }

            var existing = _unitOfWork.EmployeeRepository.GetbyUserName(request.Username.Trim());
            if (existing != null)
            {
                return BadRequest(new ServerResponse<bool>(false, "Username already exists."));
            }

            var newEmployee = new Employees
            {
                UserName = request.Username.Trim(),
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FullName = request.FullName.Trim(),
                EmployeePhone = request.EmployeePhone.Trim(),
                EmployeeAddress = request.EmployeeAddress.Trim(),
                Role = request.Role
            };

            _unitOfWork.EmployeeRepository.Add(newEmployee);
            _unitOfWork.SaveChanges();

            return Ok(new ServerResponse<bool>(true, "Registration successful. You can now log in."));
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
