using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Attribute
{
    public class AttributeVariantValueDto
    {
        [Required]
        public int AttributeId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
