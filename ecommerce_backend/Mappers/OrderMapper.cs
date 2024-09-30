using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.Order;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                Address = order.Address,
                Phone = order.Phone,
                PaymentMethod = order.PaymentMethod,
                ShippingService = order.ShippingService,
                Notes = order.Notes 
            };
        }

        public static OrderDto ToOrderDtoWithOrderDetails(this Order order)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                Address = order.Address,
                Phone = order.Phone,
                PaymentMethod = order.PaymentMethod,
                ShippingService = order.ShippingService,
                Notes = order.Notes,
                OrderDetails = order.OrderDetails.Select(o => o.ToOrderDetailDto()).ToList()
            };
        }

        public static Order ToOrderFromCreate(this CreateOrderDto orderDto)
        {
            return new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                Status = orderDto.Status,
                Address = orderDto.Address,
                Phone = orderDto.Phone,
                PaymentMethod = orderDto.PaymentMethod,
                ShippingService = orderDto.ShippingService,
                Notes = orderDto.Notes
            };
        }
    }
}