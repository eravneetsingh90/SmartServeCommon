namespace SmartServe.Domain.Models
{
	public sealed class CatalogSearchItem
	{
		public int CategoryId { get; init; }
		public int ProductId { get; init; }
		public int VariantId { get; init; }

		public string CategoryName { get; init; } = string.Empty;
		public string ProductName { get; init; } = string.Empty;
		public string VariantName { get; init; } = string.Empty;

		public decimal Price { get; init; }

		/// <summary>
		/// Precomputed, normalized text used for fast in-memory search.
		/// Example: "ice cream vanilla single scoop"
		/// </summary>
		public string SearchText { get; init; } = string.Empty;
	}
}
