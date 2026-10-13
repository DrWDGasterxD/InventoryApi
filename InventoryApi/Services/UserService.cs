using InventoryApi.Data;
using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Services
{
    public class UserService : IUserService
    {
        private readonly InventoryDbContext _context;

        public UserService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> UpdateRoleAsync(int id, string role)
        {
            var validRoles = new[]
            {
                "client",
                "admin"
            };

            if (!validRoles.Contains(role.ToLower()))
            {
                return null;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if(user is null)
            {
                return null;
            }

            user.Role = role;

            await _context.SaveChangesAsync();

            return user;


        }
    }
}
