using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class ReceiptDetail
{
    public int ReceiptId { get; set; }

    public int VariantId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public virtual Receipt Receipt { get; set; } = null!;

    public virtual ProductVariant Variant { get; set; } = null!;
}
