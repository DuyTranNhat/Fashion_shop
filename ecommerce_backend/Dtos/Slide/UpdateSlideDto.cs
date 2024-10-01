using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ecommerce_backend.Dtos.Slide
{
    public class UpdateSlideDto
    {
        // Slide ID (for updates)
        public int SlideId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string? Link { get; set; }

        // Used for file upload during update
        public IFormFile? ImageFile { get; set; } // New property for image upload

        // Used to return the image URL when retrieving slide data
        public string? ImageUrl { get; set; } // Existing image URL, if needed

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string? Description { get; set; }

        // Slide status (e.g., active or inactive)
        public bool Status { get; set; } = true; // Default status is true
    }
}
