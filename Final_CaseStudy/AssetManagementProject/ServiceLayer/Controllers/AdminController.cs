using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Controllers;

namespace ServiceLayer.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [EnableCors("AllowAll")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepo _repo;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminRepo repo, IConfiguration configuration,ILogger<AdminController> logger)
        {
            _repo = repo;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Register([FromBody] Admin admin)
        {
            var result = await _repo.AddAdminAsync(admin);
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            _logger.LogInformation("Validating and login admin...");
            var admin = await _repo.ValidateAdminAsync(login);
            if (admin == null)
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(admin);
            return Ok(new { token });
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var list = await _repo.GetAllAdminsAsync();
            return Ok(list);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminById(int id)
        {
            var admin = await _repo.GetAdminByIdAsync(id);
            if (admin == null)
                return NotFound();
            return Ok(admin);
        }

        [HttpPut]
        [Route("UpdateAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAdmin([FromBody] Admin admin)
        {
            var updated = await _repo.UpdateAdminAsync(admin);
            return Ok(updated);
        }

        [HttpDelete]
        [Route("DeleteAdminById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var result = await _repo.DeleteAdminAsync(id);
            if (!result) return NotFound();
            return Ok("Admin deleted successfully.");
        }

        [HttpPost]
        [Route("FotgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var result = await _repo.ForgotPasswordAsync(email);
            if (result == null) return NotFound("Admin not found.");
            return Ok($"Password: {result}");
        }

        [NonAction]
        private string GenerateJwtToken(Admin admin)
        {
            var claims = new[]
            {
                new Claim("adminId", admin.AdminId.ToString()),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Email, admin.Email)
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
