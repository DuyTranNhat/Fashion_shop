using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class ProductVariant
{
    public int VariantId { get; set; }

    public int? ProductId { get; set; }

    public string? VariantName { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Product? Product { get; set; }

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    public virtual ICollection<VariantAttribute> VariantAttributes { get; set; } = new List<VariantAttribute>();
}
