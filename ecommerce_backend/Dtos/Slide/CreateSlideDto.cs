using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Slide
{
    public class CreateSlideDto
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
        public IFormFile Image { get; set; } = null;
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
