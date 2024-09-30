using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.OrderDetail;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class OrderDetailMapper 
    {
        public static OrderDetailDto ToOrderDetailDto(this OrderDetail orderDetailModel)
        {
            return new OrderDetailDto
            {
                OrderDetailId = orderDetailModel.OrderDetailId,
                OrderId = orderDetailModel.OrderId,
                VariantId = orderDetailModel.VariantId,
                Quantity = orderDetailModel.Quantity,
                Price = orderDetailModel.Price,
            };
        }

        public static OrderDetail ToOrderDetailFromCreate(this CreateOrderDetailDto orderDetailDto)
        {
            return new OrderDetail
            {
                OrderId = orderDetailDto.OrderId,
                VariantId = orderDetailDto.VariantId,
                Quantity = orderDetailDto.Quantity,
                Price = orderDetailDto.Price,
            };
        }
    }
}