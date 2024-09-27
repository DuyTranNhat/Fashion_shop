using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.Customer
{
    public class CreateCustomerDto
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? ImageUrl { get; set; }

        public string Password { get; set; } = null!;

    }
}