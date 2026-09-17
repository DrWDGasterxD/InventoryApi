using System.ComponentModel.DataAnnotations;

namespace InventoryApi.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Stock {  get; set; }

        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Range (1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
