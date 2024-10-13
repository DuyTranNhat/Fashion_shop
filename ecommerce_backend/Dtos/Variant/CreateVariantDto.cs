using ecommerce_backend.Dtos.Image;
using ecommerce_backend.Dtos.Value;
using ecommerce_backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce_backend.Dtos.NewFolder
{
    public class CreateVariantDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(255)]       
        public string VariantName { get; set; } 
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal importPrice { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal salePrice { get; set; }
        [Required]
        public List<CreateVariantValueDto> Values { get; set; }
    }
}
