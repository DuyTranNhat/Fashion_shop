using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class VariantAttribute
{
    public int VariantAttributeId { get; set; }

    public int? VariantId { get; set; }

    public int? AttributeId { get; set; }

    public virtual ProductAttribute? Attribute { get; set; }

    public virtual ProductVariant? Variant { get; set; }
}
