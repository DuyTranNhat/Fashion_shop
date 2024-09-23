using ecommerce_backend.Models;

namespace ecommerce_backend.Dtos.Attribute
{
    public class AttributeDto
    {
        public int AttributeId { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<Value> Values { get; set; } = new List<Value>();
    }
}
