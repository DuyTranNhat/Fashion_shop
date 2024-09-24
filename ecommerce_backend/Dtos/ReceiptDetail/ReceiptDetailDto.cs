using ecommerce_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.ReceiptDetail
{
    public class ReceiptDetailDto
    {
        [Required]
        public int VariantId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Range(0, 99999999.99)]
        public decimal UnitPrice { get; set; }

    }
}
