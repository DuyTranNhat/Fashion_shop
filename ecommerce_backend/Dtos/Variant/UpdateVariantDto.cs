using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ecommerce_backend.Dtos.Product;
using ecommerce_backend.Dtos.Image;

namespace ecommerce_backend.Dtos.Variant
{
    public class UpdateVariantDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(255)]
        public string VariantName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [MaxLength(255)]
        public string Status { get; set; }
        [Required]
        public List<IFormFile> listFile { get; set; }

    }
}
