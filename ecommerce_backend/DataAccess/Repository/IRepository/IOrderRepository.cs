using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.Order;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<Order> UpdateStatusAsync(int id, UpdateOrderDto orderDto);
    }
}