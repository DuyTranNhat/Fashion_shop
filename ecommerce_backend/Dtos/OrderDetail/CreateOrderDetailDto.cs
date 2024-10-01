using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.OrderDetail
{
    public class CreateOrderDetailDto 
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int VariantId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}