using ecommerce_backend.Dtos.Attribute;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Value
{
    public class VariantValueDto
    {
        [Required]
        public int ValueId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Value1 { get; set; } = null!;
        public bool Status { get; set; }
        [Required]
        public AttributeVariantValueDto Attribute { get; set; } = null!;
    }
}
