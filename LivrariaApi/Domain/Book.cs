using System;
using System.ComponentModel.DataAnnotations;

namespace LivrariaApi.Domain
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Author { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}