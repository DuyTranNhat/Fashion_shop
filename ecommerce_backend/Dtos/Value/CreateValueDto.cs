using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Value
{
    public class CreateValueDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Value1 { get; set; } = null!;
    }
}
