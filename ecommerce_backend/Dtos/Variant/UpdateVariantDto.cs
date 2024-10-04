using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ecommerce_backend.Dtos.Product;
using ecommerce_backend.Dtos.Image;

namespace ecommerce_backend.Dtos.Variant
{
    public class UpdateVariantDto
    {
        [Required]
        [MaxLength(255)]
        public string VariantName { get; set; }
    }
}
