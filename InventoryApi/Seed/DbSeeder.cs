using InventoryApi.Data;
using InventoryApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAdminAsync(InventoryDbContext context)
        {
            var adminExists = await context.Users.AnyAsync(r => r.Role == "Admin");

            if (adminExists)
            {
                return;
            }

            var admin = new User
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                Role = "Admin"
            };

            var hasher = new PasswordHasher<User>();

            admin.PasswordHash = hasher.HashPassword(admin, "Admin123");

            await context.Users.AddAsync(admin);

            await context.SaveChangesAsync();
        }
    }
}
