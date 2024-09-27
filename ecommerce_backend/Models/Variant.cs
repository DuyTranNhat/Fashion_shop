using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class Variant
{
    public int VariantId { get; set; }

    public int ProductId { get; set; }

    public string VariantName { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Status { get; set; }

    public decimal ImportPrice { get; set; }

    public decimal SalePrice { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    public virtual ICollection<Value> Values { get; set; } = new List<Value>();
}
