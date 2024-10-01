using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.ProductReview;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class ProductReviewMapper
    {
        public static ProductReviewDto ToProductReviewDto(this ProductReview productReview)
        {
            return new ProductReviewDto
            {
                ReviewId = productReview.ReviewId,
                ProductId = productReview.ProductId,
                CustomerId = productReview.CustomerId,
                Rating = productReview.Rating,
                Comment = productReview.Comment,
                ReviewDate = productReview.ReviewDate,
            };
        }

        public static ProductReview ToProductReviewFromCreateDto(this CreateProductReviewDto productReviewDto)
        {
            return new ProductReview
            {
                ProductId = productReviewDto.ProductId,
                CustomerId = productReviewDto.CustomerId,
                Rating = productReviewDto.Rating,
                Comment = productReviewDto.Comment,
                ReviewDate = productReviewDto.ReviewDate
            };
        }
    }
}