using ecommerce_backend.Models;

namespace ecommerce_backend.Dtos
{
    public class CreateProductDto
    {

        public int? CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
