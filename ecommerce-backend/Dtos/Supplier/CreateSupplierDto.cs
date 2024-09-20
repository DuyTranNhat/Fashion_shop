using ecommerce_backend.Models;

namespace ecommerce_backend.Dtos.Supplier
{
    public class CreateSupplierDto
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public bool Status { get; set; }

        public string? Notes { get; set; }

    }
}
