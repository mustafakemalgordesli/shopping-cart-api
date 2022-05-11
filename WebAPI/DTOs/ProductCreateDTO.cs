using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
