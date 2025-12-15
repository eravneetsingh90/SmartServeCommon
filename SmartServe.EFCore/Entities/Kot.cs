using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class Kot
{
    public int KotId { get; set; }

    public int? OrderId { get; set; }

    public string? Status { get; set; }

    public bool? Printed { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Order? Order { get; set; }
}
