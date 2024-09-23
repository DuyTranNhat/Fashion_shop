using ecommerce_backend.Models;

namespace ecommerce_backend.Dtos.Attribute
{
    public class CreateAttributeDto
    {
        public string? Name { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();

    }
}
