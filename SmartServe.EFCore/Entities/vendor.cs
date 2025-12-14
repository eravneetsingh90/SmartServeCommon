using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

public partial class vendor
{
    [Key]
    public int vendor_id { get; set; }

    [StringLength(150)]
    public string name { get; set; } = null!;

    [StringLength(20)]
    public string? phone { get; set; }

    [StringLength(20)]
    public string? gst_no { get; set; }

    public bool? is_active { get; set; }

    //[InverseProperty("vendor")]
    //public virtual ICollection<purchase_bill> purchase_bills { get; set; } = new List<purchase_bill>();
}
