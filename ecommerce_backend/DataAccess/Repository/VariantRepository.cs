using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Dtos.Variant;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.DataAccess.Repository
{
    public class VariantRepository : Repository<Variant>, IVariantRepository
    {
        private readonly FashionShopContext _db;

        public VariantRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Variant> Edit(int id, UpdateVariantDto obj)
        {
            var existingVariant = await _db.Variants.FirstOrDefaultAsync(item => item.VariantId == id);
            if (existingVariant == null)
                return null;

            existingVariant.VariantName = obj.VariantName;
            existingVariant.importPrice = obj.importPrice;
            existingVariant.salePrice = obj.salePrice;
            existingVariant.Quantity = obj.Quantity;
            existingVariant.Status = obj.Status;

            return existingVariant;
        }
    }
}
