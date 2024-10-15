using ecommerce_backend.Dtos.Variant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.OrderDetail
{
    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public GetVariantDto Variant { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}