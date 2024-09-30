using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.Review
{
    public class ProductReviewDto
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}