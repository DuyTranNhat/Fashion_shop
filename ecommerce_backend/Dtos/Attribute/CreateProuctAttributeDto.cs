using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Attribute
{
    public class CreateProuctAttributeDto
    {
        [Required]
        public int AttributeId { get; set; }
    }
}
