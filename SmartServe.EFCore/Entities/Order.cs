using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public string? OrderNumber { get; set; }

    public string? OrderType { get; set; }

    public string? Status { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? TableId { get; set; }

    public int? StatusId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? ClosedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Kot? Kot { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual TableStatus? StatusNavigation { get; set; }

    public virtual RestaurantTable? Table { get; set; }
}
