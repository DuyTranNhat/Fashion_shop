using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class Image
{
    public int ImageId { get; set; }

    public int? VariantId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ProductVariant? Variant { get; set; }
}
