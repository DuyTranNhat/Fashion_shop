using ecommerce_backend.Dtos.Variant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.Cart
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public GetVariantDto Variant { get; set; }
        public int Quantity { get; set; }
        public decimal totalPrice => Quantity * Variant.salePrice;
        public DateTime DateAdded { get; set; }
    } 
}