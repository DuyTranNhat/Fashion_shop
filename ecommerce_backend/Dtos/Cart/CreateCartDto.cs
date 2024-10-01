using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.Cart
{
    public class CreateCartDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int VariantId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; } = 1;
        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}