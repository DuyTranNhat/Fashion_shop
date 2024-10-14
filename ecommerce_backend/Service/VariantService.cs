using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Cart;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Service.IService;

namespace ecommerce_backend.Service
{
    public class VariantService : IVariantService
    {
        private IUnitOfWork _unitOfWork;
        public VariantService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task DecreaseQuantity(int variantID, int quantityDecreased)
        {
            var variant = _unitOfWork.Variant.Get(v => v.VariantId == variantID) 
                ?? throw new NotFoundException("Variant Not Found");

            if (variant.Quantity < quantityDecreased) 
                throw new BadHttpRequestException("Cannot provide enough quantity, Please choose proper ones!");

            await _unitOfWork.Variant.UpdateQuantity(variantID, quantityDecreased);
        }
    }
}
