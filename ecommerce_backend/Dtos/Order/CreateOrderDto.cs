using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.Order
{
    public class CreateOrderDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; } = 0;
        [Required]
        [RegularExpression("^(Pending|Shipped|Delivered|Cancelled)$", ErrorMessage = "Status must be either 'Pending', 'Shipped', 'Delivered', or 'Cancelled'.")]
        public string? Status { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format.")]
        public string? Phone { get; set; }

        public string? PaymentMethod { get; set; }

        public string? ShippingService { get; set; }

        public string? Notes { get; set; }
    }
}