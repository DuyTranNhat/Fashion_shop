using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Image
{
    public class UpdateImageDto
    {
        [Required]
        [MaxLength(255)]
        public string ImageUrl { get; set; } = null!;

    }
}
