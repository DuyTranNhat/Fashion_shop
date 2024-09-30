using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.ProductReview
{
    public class CreateProductReviewDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int? Rating { get; set; }
        [Required]
        public string? Comment { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}