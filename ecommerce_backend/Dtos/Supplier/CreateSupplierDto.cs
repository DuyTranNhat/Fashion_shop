using ecommerce_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Supplier
{
    public class CreateSupplierDto
    {
        [MaxLength(255)]
        public string? Name { get; set; }

        [MaxLength(255)]
        public string? Email { get; set; }
        [MaxLength(50)]
        public string? Phone { get; set; }
        [MaxLength(500)]
        public string? Address { get; set; }

        public bool Status { get; set; }

        public string? Notes { get; set; }

    }
}
