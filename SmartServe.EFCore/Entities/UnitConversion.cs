using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class UnitConversion
{
    public string FromUnit { get; set; } = null!;

    public string ToUnit { get; set; } = null!;

    public decimal? ConversionFactor { get; set; }
}
