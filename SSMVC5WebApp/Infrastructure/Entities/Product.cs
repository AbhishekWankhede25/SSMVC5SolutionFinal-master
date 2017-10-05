using System.ComponentModel.DataAnnotations;

namespace SSMVC5WebApp.Infrastructure.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        public string PhotoUrl { get; set; }

    }
}