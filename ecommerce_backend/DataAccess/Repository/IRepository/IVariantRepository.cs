using ecommerce_backend.Dtos.Image;
using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Dtos.Variant;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IVariantRepository : IRepository<Variant>
    {
        public Task<Variant?> Edit(int id, UpdateVariantDto obj, List<UpdateImageDto> listUpdateImageDto);

        Variant? UpdateStatus(int id, string status);

        void UpdateVariantQuantity(Receipt receipt);
    }
}