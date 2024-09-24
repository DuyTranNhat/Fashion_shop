using ecommerce_backend.Dtos.ReceiptDetail;
using ecommerce_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Receipt
{
    public class ReceiptDto
    {
        [Required]
        public int ReceiptId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        [Range(0, 99999999.99)]
        public decimal TotalPrice { get; set; }
        [Required]
        public DateTime? ReceiptDate { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Status { get; set; }
        [Required]
        public virtual ICollection<ReceiptDetailDto> ReceiptDetails { get; set; } = new List<ReceiptDetailDto>();
    }
}
