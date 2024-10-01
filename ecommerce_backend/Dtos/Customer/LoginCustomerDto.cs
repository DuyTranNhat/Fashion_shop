using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.Customer
{
    public class LoginCustomerDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Role is required")]
        [RegularExpression(@"^(admin|customer)$", ErrorMessage = "Role must be either 'admin' or 'customer'")]
        public string? Role { get; set; } = "customer";
    }
}