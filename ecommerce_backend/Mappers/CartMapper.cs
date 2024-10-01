using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.Cart;

namespace ecommerce_backend.Mappers
{
    public static class CartMapper
    {
        public static CartDto ToCartDto(this Cart cartModel)
        {
            return new CartDto
            {
                CartId = cartModel.CartId,
                CustomerId = cartModel.CustomerId,
                VariantId = cartModel.VariantId,
                Quantity = cartModel.Quantity,
                DateAdded = cartModel.DateAdded
            };
        }

        public static Cart ToCartFromCreate(this CreateCartDto cartDto)
        {
            return new Cart
            {
                CustomerId = cartDto.CustomerId,
                VariantId = cartDto.VariantId,
                Quantity = cartDto.Quantity,
                DateAdded = cartDto.DateAdded
            };
        }
    }
}