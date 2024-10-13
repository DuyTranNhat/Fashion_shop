using ecommerce_backend.Dtos.Cart;
using ecommerce_backend.Dtos.Order;
using ecommerce_backend.Dtos.ProductReview;
using ecommerce_backend.Models;

namespace ecommerce_backend.Dtos.Customer
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string Password { get; set; } = null!;
    }
}