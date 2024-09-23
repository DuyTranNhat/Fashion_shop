using ecommerce_backend.Dtos.Value;
using ecommerce_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Attribute
{
    public class CreateAttributeDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public virtual ICollection<CreateValueDto> Values { get; set; } = new List<CreateValueDto>();

    }
}
