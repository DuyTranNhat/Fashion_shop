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
        public static OrderDto ToOrderDto(this Order order, string customerName)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerName = customerName,
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

        public static Order ToOrderFromCreate(this CreateOrderDto orderDto, int customerID)
        {
            return new Order
            {
                CustomerId = customerID,
                OrderDate = DateTime.Now,
                Status = "pending",
                Address = orderDto.Address,
                Phone = orderDto.Phone,
                PaymentMethod = orderDto.PaymentMethod,
                ShippingService = orderDto.ShippingService,
                Notes = orderDto.Notes
            };
        }

        public static OrderDto ToOrderDetailsDto(this Order order, IEnumerable<OrderDetail> orderDetails, IEnumerable<Models.Attribute> attributes)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerName = order.Customer.Name,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Address = order.Address,
                Phone = order.Phone,
                PaymentMethod = order.PaymentMethod,
                ShippingService = order.ShippingService,
                Notes = order.Notes,
                OrderDetails = orderDetails.Select(od => od.ToOrderDetailDto(attributes)).ToList()
            };
        }

        public static OrderDto ToOrderDetailsWithoutImagesDto(this Order order, IEnumerable<OrderDetail> orderDetails, IEnumerable<Models.Attribute> attributes)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerName = order.Customer.Name,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                Address = order.Address,
                Phone = order.Phone,
                PaymentMethod = order.PaymentMethod,
                ShippingService = order.ShippingService,
                Notes = order.Notes,
                OrderDetails = orderDetails.Select(od => od.ToOrderDetailDto(attributes)).ToList()
            };
        }

        

        public static Order ToOrderFromCreate(this CreateOrderDto orderDto, decimal totalAmount, int customerID)
        {
            return new Order
            {
                CustomerId = customerID,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = "Pending",
                Address = orderDto.Address,
                Phone = orderDto.Phone,
                PaymentMethod = orderDto.PaymentMethod ?? "COD",
                ShippingService = orderDto.ShippingService ?? "GRAB",
                Notes = orderDto.Notes
            };
        }
    }
}