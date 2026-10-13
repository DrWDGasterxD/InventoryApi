using InventoryApi.Models;

namespace InventoryApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User?> UpdateRoleAsync(int id, string role);
    }
}
