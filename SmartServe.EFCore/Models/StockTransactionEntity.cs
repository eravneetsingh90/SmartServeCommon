using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class StockTransactionEntity
{
    public int Id { get; set; }

    public int TenantId { get; set; }

    public int StockId { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Quantity { get; set; }

    public string Reason { get; set; } = null!;

    public string? ReferenceType { get; set; }

    public int? ReferenceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual StockEntity Stock { get; set; } = null!;

    public virtual TenantEntity Tenant { get; set; } = null!;
}
