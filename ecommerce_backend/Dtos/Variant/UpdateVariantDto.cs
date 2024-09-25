using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ecommerce_backend.Dtos.Product;

namespace ecommerce_backend.Dtos.Variant
{
    public class UpdateVariantDto
    {
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
        public int Quantity { get; set; }
        [Required]
        public string Status { get; set; }

    }
}
