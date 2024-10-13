using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.Cart;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        public Task<Cart> increaseQuantity(int idCart);
        public Task<Cart?> decreaseQuantity(int idCart);

        public Task<Cart> UpdateAsync(CreateCartDto cartDto);


    }
}