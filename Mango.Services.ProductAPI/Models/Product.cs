using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        [Required]
        [Range(0,1000)]
        public double ProductPrice { get; set; }
        public string ProductCategoryName { get; set; } = string.Empty;
        public string ProductImageUrl { get; set; } = string.Empty;
    }
}
