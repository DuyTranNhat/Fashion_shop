using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Dtos.Variant;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IVariantRepository : IRepository<Variant>
    { 
        public Task<Variant> Edit(int id, UpdateVariantDto obj);
    }
}