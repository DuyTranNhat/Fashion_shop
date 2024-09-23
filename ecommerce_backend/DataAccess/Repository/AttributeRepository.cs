using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.DataAccess.Repository
{
    public class AttributeRepository : Repository<Attribute>, IAttributeRepository
    {
        private readonly FashionShopDemoContext _db;
        public AttributeRepository(FashionShopDemoContext db) : base(db)
        {
            _db = db;
        }

        

        public void Update(Attribute obj)
        {
            throw new NotImplementedException();
        }
    }
}
