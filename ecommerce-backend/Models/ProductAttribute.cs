using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class ProductAttribute
{
    public int AttributeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();

    public virtual ICollection<VariantAttribute> VariantAttributes { get; set; } = new List<VariantAttribute>();
}
