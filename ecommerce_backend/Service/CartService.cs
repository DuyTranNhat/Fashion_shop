using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Cart;
using ecommerce_backend.Mappers;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Service
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CartDto> GetByCustomerIdAsync(int customerId)
        {
            var cartModel = _unitOfWork.Cart.Get(c => c.CustomerId == customerId, includeProperties: "Variant.Images,Variant.VariantValues");
            var attributes = _unitOfWork.Attribute.GetAll(includeProperties: "Values");
            if (cartModel == null) throw new BadHttpRequestException("Not found a customer id");
            return cartModel.ToCartDto(attributes);
        }
    }
}
