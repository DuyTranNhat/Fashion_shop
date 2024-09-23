using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Product;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly FashionShopContext _db;
        public ProductRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Product> Edit(int id, updateProductDto obj)
        {
            var existingProduct = await _db.Products.FirstOrDefaultAsync(item => item.ProductId == id);
            if (existingProduct == null)
                return null;

            existingProduct.CategoryId = obj.CategoryId;
            existingProduct.SupplierId = obj.SupplierId;
            existingProduct.Name = obj.Name;
            existingProduct.Description = obj.Description;

            _db.SaveChanges();
            return existingProduct;

        }
    }
}

