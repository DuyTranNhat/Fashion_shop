using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Slide
{
    public class UpdateSlideDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Title { get; set; } = null!;
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string? Link { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string? Image { get; set; }
    }
}
