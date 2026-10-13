using System.ComponentModel.DataAnnotations;

namespace InventoryApi.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
