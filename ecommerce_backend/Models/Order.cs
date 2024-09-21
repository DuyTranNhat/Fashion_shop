﻿using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Status { get; set; }

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }

    public string? PaymentMethod { get; set; }

    public string? ShippingService { get; set; }

    public string? Notes { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
