using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly FashionShopContext _db;
        public CategoryRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }


        public async Task<List<Category>> getAllRecursive()
        {
            return _db.Categories.Where(c => c.ParentCategoryId == null).
                Include(c => c.InverseParentCategory).ThenInclude(c => c.InverseParentCategory).ToList();
        }

        public async Task<Category> GetById(int id)
        {
            var existingCategory = await _db.Categories.FirstOrDefaultAsync(item => item.CategoryId == id);

            if (existingCategory == null)
            {
                return null;
            }

            return existingCategory;
        }

       

        public void Update(Category obj)
        {
            dbSet.Update(obj);
        }


    }
}
