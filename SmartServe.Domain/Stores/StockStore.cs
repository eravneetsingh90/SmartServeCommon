using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Constants;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class StockStore : BaseStore<Stock>, IStockStore
	{
		public StockStore(SmartServeDbContext db) : base(db)
		{
			
		}
		public bool CanHandle(ProductVariant variant)
		=> variant.StockMode == StockMode.SEALED;


		public async Task ConsumeAsync(OrderItem item, int orderId)
		{
			var stock = await _db.Stocks.FirstOrDefaultAsync(s => s.VariantId == item.VariantId);

			if (stock == null)
				throw new InvalidOperationException(
					$"No stock record found for variant {item.VariantId}");

			if (stock.Quantity < item.Quantity)
				throw new InvalidOperationException(
					$"Insufficient stock for variant {item.VariantId}");

			stock.Quantity -= item.Quantity;

			_db.StockTransactions.Add(new StockTransaction
			{
				VariantId = item.VariantId,
				ChangeQty = -item.Quantity,
				Reason = "SALE",
				ReferenceId = orderId,
				CreatedAt = DateTime.UtcNow
			});
		}
	}
}
