using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[PrimaryKey("from_unit", "to_unit")]
public partial class unit_conversion
{
    [Key]
    [StringLength(20)]
    public string from_unit { get; set; } = null!;

    [Key]
    [StringLength(20)]
    public string to_unit { get; set; } = null!;

    [Precision(10, 4)]
    public decimal? conversion_factor { get; set; }
}
