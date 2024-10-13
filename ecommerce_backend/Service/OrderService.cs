using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Cart;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Dtos.Order;
using ecommerce_backend.Mappers;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICartService _cartService;
        private readonly ICustomerService _customerService;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<OrderDto>> GetAllOrderAsync()
        {
            var orders = _unitOfWork.Order.GetAll(includeProperties: "OrderDetails");
            var orderDtos = orders.Select(order=>order.ToOrderDto());
            return orderDtos;
        }



        public async Task<OrderDto> GetByIdWithOrderDetailAsync(int orderId)
        {
            var order = _unitOfWork.Order.Get(o => o.OrderId == orderId, includeProperties:"Order_Detai");
            if (order == null) throw new BadHttpRequestException("Not found an order id");
            return order.ToOrderDto();
        }

        public async Task<CheckOutDto> CheckOut(int customerId)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(customerId);
                var cart = await _cartService.GetByCustomerIdAsync(customerId);
                return new CheckOutDto { Cart = cart, Customer = customer };
            } catch (BadHttpRequestException ex) {
                throw new BadHttpRequestException(ex.Message);
            }   
        }
        //public async Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        //{
        //    if (_unitOfWork.Customer.Get(c => c.CustomerId == orderDto.CustomerId) == null);
        //    var orderModel = orderDto.ToOrderFromCreate();
        //    _unitOfWork.Order.Add(orderModel);
        //    _unitOfWork.Save();
        //}
        //public async Task<OrderDto> PayOrder([FromRoute] int id, [FromBody] UpdateOrderDto orderDto)
        //{

        //}
    }
}
