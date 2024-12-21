namespace Mango.Web.Models
{
    public class ProductDto
    {
        public int ProductId   { get; set; }    
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public double ProductPrice { get; set; }
        public string ProductCategoryName { get; set; } = string.Empty;     
        public string ProductImageUrl { get; set; } = string.Empty;

    }
}
