using System.ComponentModel.DataAnnotations;

namespace LivrariaApi.API.DTOs
{
    public class CreateBookDto
    {
        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Author { get; set; }

        [MaxLength(50)]
        public required string Genre { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}