using Microsoft.AspNetCore.Mvc;
using RailEaseAPI.Data;
using RailEaseAPI.Models;

namespace RailEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existing = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existing != null)
                return BadRequest("Email already registered!");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("Register successful!");
        }

        // Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var existing = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (existing == null)
                return Unauthorized("Invalid email or password!");

            return Ok(new { message = "Login successful!", id = existing.Id, name = existing.Name, email = existing.Email, role = existing.Role });
        }

        // Update Profile
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("User not found!");

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Profile updated!", id = user.Id, name = user.Name, email = user.Email, role = user.Role });
        }

        // Change Password
        [HttpPut("change-password/{id}")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("User not found!");

            if (user.Password != dto.CurrentPassword)
                return BadRequest("Current password is incorrect!");

            if (dto.NewPassword != dto.ConfirmPassword)
                return BadRequest("New passwords do not match!");

            user.Password = dto.NewPassword;
            await _context.SaveChangesAsync();

            return Ok("Password changed successfully!");
        }
    }
}