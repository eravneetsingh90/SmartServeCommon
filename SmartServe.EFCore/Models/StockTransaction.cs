using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class StockTransaction
{
    public int Id { get; set; }

    public int StockId { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Quantity { get; set; }

    public string Reason { get; set; } = null!;

    public string? ReferenceType { get; set; }
	public int? ReferenceId { get; set; }

	public DateTime? CreatedAt { get; set; }

    public virtual Stock Stock { get; set; } = null!;
}
