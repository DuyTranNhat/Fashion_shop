using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Order;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly FashionShopContext _db;

        public OrderRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Order> UpdateStatusAsync(int id, UpdateOrderDto orderDto)
        {
            var existingOrder = _db.Orders.FirstOrDefault(o => o.OrderId == id);
            if(existingOrder == null) return null;
            existingOrder.Status = orderDto.Status;
            await _db.SaveChangesAsync();
            return existingOrder;
        }
    }
}