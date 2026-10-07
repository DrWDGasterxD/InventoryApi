using InventoryApi.DTOs;

namespace InventoryApi.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto);

        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    }
}
