using ecommerce_backend.Dtos.Image;
using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Dtos.Variant;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IVariantRepository : IRepository<Variant>
    {
        public Task<Variant?> Edit(int id, UpdateVariantDto obj);
        Variant? UpdateStatus(int id, string status);
        Task DecreaseQuantity(int variantID, int quantityDecreased);
        Task UpdateQuantity(int variantID, int quantityDecreased);

        void UpdateVariantQuantity(Receipt receipt);
    }
}