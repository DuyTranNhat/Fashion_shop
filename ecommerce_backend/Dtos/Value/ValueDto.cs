using ecommerce_backend.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Value
{
    public class ValueDto
    {
        [Required]
        public int ValueId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Value1 { get; set; } = null!;
        public bool Status { get; set; }
        public int AttributeID { get; set; }
        public string AttributeName { get; set; }   
    }
}
