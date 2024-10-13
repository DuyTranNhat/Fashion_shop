using ecommerce_backend.Dtos.Attribute;
using ecommerce_backend.Dtos.Category;
using ecommerce_backend.Dtos.Supplier;

namespace ecommerce_backend.Dtos.Product
{
    public class updateProductDto
    {

        public int? CategoryId { get; set; }

        public int? SupplierId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<UpdateProductAttributeDto> Attributes { get; set; } = new List<UpdateProductAttributeDto>();

    }
}
