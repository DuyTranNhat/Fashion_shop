using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        private readonly FashionShopContext _db;
        public ImageRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }
    }
}
