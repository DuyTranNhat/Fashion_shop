using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository
{

    public class VariantValueRepository : Repository<VariantValue>, IVariantValueRepository
    {
        private readonly FashionShopContext _db;

        public VariantValueRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }
    }




}
