using System.ComponentModel.DataAnnotations;

namespace InventoryApi.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
