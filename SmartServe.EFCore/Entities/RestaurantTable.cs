using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class RestaurantTable
{
    public int TableId { get; set; }

    public string? DisplayName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
