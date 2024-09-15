using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class AttributeValue
{
    public int ValueId { get; set; }

    public int? AttributeId { get; set; }

    public string? Value { get; set; }

    public virtual ProductAttribute? Attribute { get; set; }
}
