using InventoryApi.Models;

namespace InventoryApi.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
