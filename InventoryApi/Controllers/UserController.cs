using InventoryApi.Models;
using InventoryApi.DTOs;
using InventoryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize (Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.GetUsersAsync();

            var usersDto = users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Role = u.Role,
                Email = u.Email
            });

            return Ok(usersDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateRole(int id, UpdateUserDto dto)
        {
            var user = await _userService.UpdateRoleAsync(id, dto.Role);

            if(user is null)
            {
               return BadRequest();
            }

            return NoContent();
        }
    }
}
