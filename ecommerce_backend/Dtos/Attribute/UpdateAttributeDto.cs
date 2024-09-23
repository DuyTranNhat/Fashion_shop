using ecommerce_backend.Dtos.Value;
using ecommerce_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Attribute
{
    public class UpdateAttributeDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Name { get; set; }
        public virtual ICollection<UpdateValueDto> Values { get; set; } = new List<UpdateValueDto>();
    }
}
