using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class TableStatusEntity
{
    public int Id { get; set; }

    public string StatusCode { get; set; } = null!;

    public string? StatusName { get; set; }

    public string? ColorHex { get; set; }

    public virtual ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}
