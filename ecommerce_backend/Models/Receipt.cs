using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class Receipt
{
    public int ReceiptId { get; set; }

    public DateTime CreateDate { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? ReceiptDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
}
