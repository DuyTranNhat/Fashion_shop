using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly FashionShopContext _db;
        public CategoryRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }



        public void Update(Category obj)
        {
            dbSet.Update(obj);
        }
    }
}
