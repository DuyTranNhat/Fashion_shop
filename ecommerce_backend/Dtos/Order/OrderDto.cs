using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.OrderDetail;
using ecommerce_backend.Models;

namespace ecommerce_backend.Dtos.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Status { get; set; }

        public string Address { get; set; } = null!;

        public string? Phone { get; set; }

        public string? PaymentMethod { get; set; } 

        public string? ShippingService { get; set; }

        public string? Notes { get; set; }
        public virtual ICollection<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();
    }
}