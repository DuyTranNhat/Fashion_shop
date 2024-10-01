using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly FashionShopContext _db;

        public  OrderDetailRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }
    }
}