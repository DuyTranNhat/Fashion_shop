using ecommerce_backend.Dtos.Attribute;
using ecommerce_backend.Dtos.Category;
using ecommerce_backend.Dtos.Supplier;
using ecommerce_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Product
{
    public class CreateProductDto
    {

        public int? CategoryId { get; set; }

        public int? SupplierId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<CreateProuctAttributeDto> Attributes { get; set; } = new List<CreateProuctAttributeDto>();

    }
}
