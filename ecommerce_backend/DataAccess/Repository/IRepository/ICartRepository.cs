using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.Cart;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        public Task<Cart> UpdateAsync(int id, UpdateCartDto cartDto);
    }
}