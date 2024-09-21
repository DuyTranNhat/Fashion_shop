namespace ecommerce_backend.Models
{
    
    public class ProductAttribute
    {
        public int ProductId { get; set; }
        public int AttributeId { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public Attribute Attribute { get; set; }
    }
}
