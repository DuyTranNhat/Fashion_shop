using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository
{
    public class ReceiptRepository : Repository<Receipt>, IReceiptRepository
    {
        private readonly FashionShopContext _db;
        public ReceiptRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }
    }
}
