using ecommerce_backend.Dtos.Value;
using ecommerce_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Attribute
{
    public class AttributeDto
    {
        [Required]
        public int AttributeId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public virtual ICollection<ValueDto> Values { get; set; } = new List<ValueDto>();
    }
}
