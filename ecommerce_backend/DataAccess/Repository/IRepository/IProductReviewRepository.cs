using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.ProductReview;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IProductReviewRepository : IRepository<ProductReview>
    {
        public Task<ProductReview> UpdateAsync(int id, UpdateProductReviewDto updateProductReviewDto);
    }
}