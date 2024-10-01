using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.Order
{
    public class UpdateOrderDto 
    {
        [Required]
        [RegularExpression("^(pending|delivering|received|cancelled|paid)$", ErrorMessage = "Status must be either 'pending', 'delivering', 'received', 'paid' or 'cancelled'.")]
        public string? Status { get; set; } = "pending";
    }
}