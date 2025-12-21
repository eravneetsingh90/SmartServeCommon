using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class RestaurantTable
{
    public int TableId { get; set; }

    public string DisplayName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
