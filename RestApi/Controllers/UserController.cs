using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RestApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IRepository<User> _repository;

        public UsersController(IRepository<User> repository)
        {
            _repository = repository;
        }

        // Admin can view all users
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.GetAllAsync();
            return Ok(users);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var currentUsername = User.Identity.Name;

            if (currentUserRole == "User" && user.Username != currentUsername)
            {
                return Forbid();
            }


            return Ok(user);
        }

        // Admin can create users
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _repository.AddAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // User can update own profile, Admin can update any user
        [Authorize(Roles = "User,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var currentUsername = User.Identity.Name;

            if (id != user.Id || (currentUserRole == "User" && user.Username != currentUsername))
            {
                return BadRequest();
            }
            await _repository.UpdateAsync(user);
            return NoContent();
        }

        // Admin can delete users
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }

}
