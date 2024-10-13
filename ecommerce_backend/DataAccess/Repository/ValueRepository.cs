using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Value;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.DataAccess.Repository
{
    public class ValueRepository : Repository<Value>, IValueRepository
    {
        private readonly FashionShopContext _db;
        public ValueRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }

        //public void CreateVariantValue(Variant variant, ICollection<CreateVariantValueDto> variantValue)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
