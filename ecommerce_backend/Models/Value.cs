﻿using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class Value
{
    public int ValueId { get; set; }

    public int AttributeId { get; set; }

    public string Value1 { get; set; } = null!;

    public bool Status { get; set; }

    public virtual Attribute Attribute { get; set; } = null!;
    public virtual ICollection<Variant> Variants { get; set; } = new List<Variant>();

    public virtual ICollection<VariantValue> VariantValues { get; set; } = new List<VariantValue>();

}
