using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [EnableCors("AllowAll")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepo _repo;
        private readonly IConfiguration _configuration;

        public EmployeeController(IEmployeeRepo repo, IConfiguration configuration, ILogger<EmployeeController> logger)
        {
            _repo = repo;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> EmployeeRegistration([FromBody] Employee employee)
        {
            _logger.LogInformation("Registering employee...");
            if (employee != null)
            {
                var result = await _repo.AddEmployeeAsync(employee);
                return Created(HttpContext.Request.Path,result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            _logger.LogInformation("Login employee...");
            var emp = await _repo.ValidateEmployeeAsync(login);
            if (emp == null)
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(emp);
            return Ok(new { token });
        }

        [HttpGet("profile")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetOwnProfile()
        {
            _logger.LogInformation("Fetching my profile..");
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email)) return Unauthorized("Token missing email claim");

            var emp = await _repo.GetEmployeeByEmailAsync(email);
            if (emp == null) return NotFound("Employee not found");

            return Ok(emp);
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllEmployees()
        {
            _logger.LogInformation("Fetching all employees...");
            var list = await _repo.GetAllEmployeesAsync();
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetEmployeeById/{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            _logger.LogInformation("Fetching Employee with ID: {Id}", id);
            var emp = await _repo.GetEmployeeByIdAsync(id);
            if (emp == null)
            {
                _logger.LogWarning("Employee with ID {Id} not found",id);
                return NotFound();
            }
            _logger.LogInformation("Employee details: {@Employee}", emp);    
            return Ok(emp);
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            _logger.LogInformation("Updating employee...");
            var updated = await _repo.UpdateEmployeeAsync(employee);
            if (updated == null) return BadRequest();
            return Accepted(HttpContext.Request.Path,updated);
        }

        [HttpDelete("DeleteEmployeeById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _logger.LogWarning("Attempting to delete employee with ID: {Id}", id);

            var result = await _repo.DeleteEmployeeAsync(id);
            if (!result)
            {
                _logger.LogError("Failed to delete employee with ID: {Id}", id);
                return BadRequest("Delete failed.");
            }

            _logger.LogInformation("Successfully deleted employee with ID: {Id}", id);
            return Ok("Deleted successfully.");
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var result = await _repo.ForgotPasswordAsync(email);
            if (result == null) return NotFound("Employee not found.");
            return Ok($"Password: {result}");
        }
        [NonAction]
        private string GenerateJwtToken(Employee emp)
        {
            var claims = new[]
            {
                new Claim("employeeId", emp.EmployeeId.ToString()),
                new Claim(ClaimTypes.Role, "Employee"),
                new Claim(ClaimTypes.Email, emp.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
