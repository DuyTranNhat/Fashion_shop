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
        public void CreateVariantValue(Variant variant, ICollection<CreateVariantValueDto> variantValue)
        {
            variantValue.ToList().ForEach(v =>
            {
                var value = _db.Values.Include(v=>v.Attribute).FirstOrDefault(value => value.ValueId == v.ValueId);
                variant.Values.Add(value);
            });
        }
    }
}
