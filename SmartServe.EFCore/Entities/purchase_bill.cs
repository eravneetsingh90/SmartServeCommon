using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Index("vendor_id", Name = "idx_purchase_bill_vendor")]
public partial class purchase_bill
{
    [Key]
    public int purchase_bill_id { get; set; }

    public int? vendor_id { get; set; }

    [StringLength(50)]
    public string? bill_number { get; set; }

    public DateOnly? bill_date { get; set; }

    [Precision(10, 2)]
    public decimal? total_amount { get; set; }

    public string? image_url { get; set; }

    [StringLength(20)]
    public string? ocr_status { get; set; }

    public DateTime? created_at { get; set; }

    //[InverseProperty("purchase_bill")]
    //public virtual ICollection<purchase_bill_parsed_item> purchase_bill_parsed_items { get; set; } = new List<purchase_bill_parsed_item>();

    //[InverseProperty("purchase_bill")]
    //public virtual ICollection<purchase_item> purchase_items { get; set; } = new List<purchase_item>();

    //[ForeignKey("vendor_id")]
    //[InverseProperty("purchase_bills")]
    //public virtual vendor? vendor { get; set; }
}
