using ecommerce_backend.Dtos.Value;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Variant
{
    public class ValueVariantRequestDTO
    {
        [Required]
        public List<CreateVariantValueDto> Values { get; set; }
    }
}
