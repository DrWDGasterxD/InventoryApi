using InventoryApi.Data;
using InventoryApi.DTOs;
using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace InventoryApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly InventoryDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(InventoryDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var userExists = await _context.Users.AnyAsync(e => e.Email == registerDto.Email);

            if (userExists)
            {
                return false;
            }

            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, registerDto.Password);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == loginDto.Email);

            if(user is null)
            {
                return null;
            }
            

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

            if(result == PasswordVerificationResult.Success)
            {

                var token = _jwtService.GenerateToken(user);

                return new AuthResponseDto
                {
                    Token = token
                };
            }

            return null;
        }


        
    }
}
