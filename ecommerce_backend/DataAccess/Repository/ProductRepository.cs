using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Attribute;
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
            var existingProduct = await _db.Products.Include(item=>item.Attributes).FirstOrDefaultAsync(item => item.ProductId == id);
            if (existingProduct == null)
                return null;

            existingProduct.CategoryId = obj.CategoryId;
            existingProduct.SupplierId = obj.SupplierId;
            existingProduct.Name = obj.Name;
            existingProduct.Description = obj.Description;
            existingProduct.Attributes.Clear();
            obj.Attributes.ToList().ForEach(a =>
            {
                var attribute = _db.Attributes.FirstOrDefault(x => x.AttributeId == a.AttributeId);
                existingProduct.Attributes.Add(attribute);
            });

            _db.SaveChanges();
            return existingProduct;

        }
    }
}

