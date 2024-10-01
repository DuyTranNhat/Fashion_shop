using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.ProductReview;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.DataAccess.Repository
{
    public class ProductReviewRepository : Repository<ProductReview>, IProductReviewRepository
    {
        private readonly FashionShopContext _db;

        public ProductReviewRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ProductReview> UpdateAsync(int id, UpdateProductReviewDto updateProductReviewDto)
        {
            var existingProductReview = await _db.ProductReviews.FirstOrDefaultAsync(p => p.ReviewId == id);
            if (existingProductReview == null) return null;
            existingProductReview.Rating = updateProductReviewDto.Rating;
            existingProductReview.Comment = updateProductReviewDto.Comment;
            await _db.SaveChangesAsync();
            return existingProductReview;
        }
    }
}