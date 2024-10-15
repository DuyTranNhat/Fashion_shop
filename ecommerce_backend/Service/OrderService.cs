using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Cart;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Dtos.Order;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICartService _cartService;
        private readonly ICustomerService _customerService;
        private readonly IVariantService _variantService;


        public OrderService(IUnitOfWork unitOfWork, IVariantService variantService, ICustomerService customerService)
        {
            _unitOfWork = unitOfWork;
            _variantService = variantService;
            _customerService = customerService;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrderAsync(int idCustomer)
        {
            var customer = _unitOfWork.Customer.Get(c => idCustomer == idCustomer)
                ?? throw new NotFoundException("Customer not found");
            var orders = _unitOfWork.Order.GetAll(includeProperties: "OrderDetails");
            var orderDtos = orders.Select(order => order.ToOrderDto(customer.Name));
            return orderDtos;
        }



        public async Task<OrderDto> GetByIdWithOrderDetailAsync(int orderId)
        {
            var order = _unitOfWork.Order.Get(o => o.OrderId == orderId, 
                includeProperties: "OrderDetails.Variant.VariantValues.Value,Customer") 
                ?? throw new BadHttpRequestException("Not found an order id");

            var attributes = _unitOfWork.Attribute.GetAll(includeProperties: "Values");

            var orderDTO = order.ToOrderDetailsWithoutImagesDto(order.OrderDetails, attributes);
            return orderDTO;

        }

        public async Task<CheckOutDto> GerDetailsByID(int customerId)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(customerId);
                var cart = await _cartService.GetByCustomerIdAsync(customerId);
                return new CheckOutDto { Cart = cart, Customer = customer };
            }
            catch (BadHttpRequestException ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
        }

        public async Task<OrderDto> createOrderAsync(int customerID, CreateOrderDto orderDto)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var customer = _unitOfWork.Customer.Get(c => c.CustomerId == customerID)
                        ?? throw new NotFoundException("Customer Not Found");

                    var order = orderDto.ToOrderFromCreate(customerID);
                    _unitOfWork.Order.Add(order);
                    _unitOfWork.Save();

                    var cartList = _unitOfWork.Cart.GetAll(c => c.CustomerId == customerID);

                    if (!cartList.Any())
                        throw new NotFoundException("Cannot order due to empty cart!");

                    decimal total = 0;

                    foreach (var itemCart in cartList)
                    {
                        var variant = _unitOfWork.Variant.Get(v => v.VariantId == itemCart.VariantId)
                            ?? throw new NotFoundException("Variant Not Found");

                        _unitOfWork.OrderDetail.Add(new OrderDetail
                        {
                            OrderId = order.OrderId,
                            VariantId = itemCart.VariantId,
                            Quantity = itemCart.Quantity,
                            Price = variant.SalePrice
                        });



                        total += variant.SalePrice * itemCart.Quantity;

                        await _variantService.DecreaseQuantity(variant.VariantId, itemCart.Quantity);
                    }

                    order.TotalAmount = total;

                    _unitOfWork.Save();

                    _unitOfWork.Cart.RemoveRange(cartList);

                    _unitOfWork.Save();

                    await transaction.CommitAsync();

                    return order.ToOrderDto(customer.Name);
                }
                catch (BadHttpRequestException ex)
                {
                    throw new BadHttpRequestException(ex.Message);
                }
                catch (NotFoundException ex)
                {
                    throw new NotFoundException(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
