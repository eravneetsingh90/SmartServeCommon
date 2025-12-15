using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class TableStatus
{
    public int StatusId { get; set; }

    public string StatusCode { get; set; } = null!;

    public string? StatusName { get; set; }

    public string? ColorHex { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
