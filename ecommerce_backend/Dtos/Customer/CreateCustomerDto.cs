using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.Customer
{
    public class CreateCustomerDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Address { get; set; }

        public string? ImageUrl { get; set; }
        [Required]
        
        public string Password { get; set; } = null!;

    }
}