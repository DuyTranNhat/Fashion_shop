﻿using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int VariantId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Variant Variant { get; set; } = null!;
}
