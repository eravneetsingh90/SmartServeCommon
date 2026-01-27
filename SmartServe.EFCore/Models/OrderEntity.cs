using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class OrderEntity
{
    public int Id { get; set; }

    public string? OrderNumber { get; set; }

    public string? OrderType { get; set; }

    public int? TableId { get; set; }

    public int? StatusId { get; set; }
	public decimal? OriginalAmount { get; set; }
	public decimal? TotalAmount { get; set; }

    public string? DiscountType { get; set; }

    public decimal? DiscountValue { get; set; }

    public string? DiscountReason { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public virtual ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();

    public virtual ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();

    public virtual TableStatusEntity? Status { get; set; }

    public virtual RestaurantTableEntity? Table { get; set; }
}
