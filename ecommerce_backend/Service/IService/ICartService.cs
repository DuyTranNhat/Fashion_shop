using ecommerce_backend.Dtos.Cart;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Service.IService
{
    public interface ICartService
    {
        public Task<CartDto> GetByCustomerIdAsync(int customerId);
    }
}
