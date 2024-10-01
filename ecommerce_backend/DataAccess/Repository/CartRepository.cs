using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Cart;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.DataAccess.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly FashionShopContext _db;

        public CartRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Cart> UpdateAsync(int id, UpdateCartDto cartDto)
        {
            var existingCart = await _db.Cart.FirstOrDefaultAsync(c => c.CartId == id);
            if (existingCart == null) return null;
            existingCart.Quantity = cartDto.Quantity;
            existingCart.DateAdded = cartDto.DateAdded;
            await _db.SaveChangesAsync();
            return existingCart;
        }
    }
}