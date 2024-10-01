using ecommerce_backend.Dtos.Attribute;
using ecommerce_backend.Dtos.Category;
using ecommerce_backend.Dtos.Supplier;
using ecommerce_backend.Models;

namespace ecommerce_backend.Dtos.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public int? CategoryId { get; set; }

        public int? SupplierId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public virtual CategoryDto? CategoryDto { get; set; }

        public virtual SupplierDto? SupplierDto { get; set; }

        public virtual ICollection<AttributeDto> Attributes { get; set; } = new List<AttributeDto>();
    }
}
