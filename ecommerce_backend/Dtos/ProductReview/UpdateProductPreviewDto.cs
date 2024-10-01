using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.ProductReview
{
    public class UpdateProductReviewDto
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}