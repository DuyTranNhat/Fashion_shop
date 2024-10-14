using ecommerce_backend.Dtos.Cart;

namespace ecommerce_backend.Service.IService
{
    public interface IVariantService
    {
        Task DecreaseQuantity(int variantID, int quantityDecreased);

    }
}
