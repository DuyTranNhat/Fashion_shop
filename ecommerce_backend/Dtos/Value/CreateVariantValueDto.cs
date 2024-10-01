using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Value
{
    public class CreateVariantValueDto
    {
        [Required]
        public int ValueId { get; set; }
    }
}
