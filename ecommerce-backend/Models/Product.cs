using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? CategoryId { get; set; }

    public int? SupplierId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<CampaignProduct> CampaignProducts { get; set; } = new List<CampaignProduct>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual Supplier? Supplier { get; set; }
}
