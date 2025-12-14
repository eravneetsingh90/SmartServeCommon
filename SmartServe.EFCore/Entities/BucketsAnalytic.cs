using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Table("buckets_analytics", Schema = "storage")]
public partial class BucketsAnalytic
{
    public string name { get; set; } = null!;

    public string format { get; set; } = null!;

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    [Key]
    public Guid id { get; set; }

    public DateTime? deleted_at { get; set; }
}
