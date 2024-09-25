using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Image
{
    public class CreateImageDto
    {

        [Required]
        [MaxLength(255)]
        public string ImageUrl { get; set; } = null!;

    }
}
