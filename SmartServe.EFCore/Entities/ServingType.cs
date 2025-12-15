using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class ServingType
{
    public int ServingTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
