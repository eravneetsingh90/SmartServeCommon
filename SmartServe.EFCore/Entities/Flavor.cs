using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class Flavor
{
    public int FlavorId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
