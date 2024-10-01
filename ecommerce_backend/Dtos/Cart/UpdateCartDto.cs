using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.Cart
{
    public class UpdateCartDto
    {
        [Required]
        public int Quantity { get; set; } = 1;
        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}