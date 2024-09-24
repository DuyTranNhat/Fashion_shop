using ecommerce_backend.Dtos.ReceiptDetail;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dtos.Receipt
{
    public class UpdateReceiptDto
    {
        [Required]
        public DateTime ReceiptDate { get; set; }
        [Required]
        public virtual ICollection<ReceiptDetailDto> ReceiptDetails { get; set; } = new List<ReceiptDetailDto>();
    }
}
