using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ecommerce_backend.Dtos.Slide
{
    public class SlideDto
    {
        // Slide ID (for updates or retrieval)
        public int SlideId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string? Link { get; set; }

        // Used for file upload during creation or update
        public IFormFile? ImageFile { get; set; }

        // Used to return the image URL when retrieving slide data
        public string? ImageUrl { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string? Description { get; set; }

        // Slide status (e.g., active or inactive)
        public bool Status { get; set; }
    }
}
