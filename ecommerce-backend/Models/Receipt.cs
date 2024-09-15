using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class Receipt
{
    public int ReceiptId { get; set; }

    public DateTime? ReceiptDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
}
