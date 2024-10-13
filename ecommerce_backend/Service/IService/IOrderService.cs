using ecommerce_backend.Dtos.Cart;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Dtos.Order;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Service.IService
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDto>> GetAllOrderAsync();
        public Task<OrderDto> GetByIdWithOrderDetailAsync(int orderId);
        public Task<CheckOutDto> CheckOut(int customerId);
        //public Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto);
        //public Task<OrderDto> PayOrder([FromRoute] int id, [FromBody] UpdateOrderDto orderDto);

    }
}
