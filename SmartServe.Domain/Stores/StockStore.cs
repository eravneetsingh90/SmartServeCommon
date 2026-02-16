using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class StockStore : BaseStore<StockEntity>, IStockStore
	{

		public StockStore(SmartServeDbContext db) : base(db)
		{
		}

		public async Task<List<StockEntity>> GetStockAsync()
		{
			return await Set
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<StockEntity?> GetStockAsync(int referenceId)
		{
			return await Set
				.FirstOrDefaultAsync(x =>
					x.VariantId == referenceId);
		}
		public async Task<StockEntity?> GetStockAsync(string itemType, int referenceId)
		{
			return await Set
				.FirstOrDefaultAsync(x =>
					x.ItemType == itemType &&
					x.VariantId == referenceId);
		}
		public async Task<List<StockEntity>> GetAllStockAsync()
		{
			return await Set
				.AsNoTracking()
				.Include(s => s.Variant)
					.ThenInclude(v => v.Product)
						.ThenInclude(s => s.Category)
				.Where(x => x.IsActive == true)
				.ToListAsync();
		}
		public async Task<List<CurrentStock>> GetCurrentStockAsync()
		{
			var result =
				await (
					from stock in Db.Stocks.AsNoTracking()
					where stock.IsActive == true

					join variant in Db.ProductVariants.AsNoTracking()
						on stock.VariantId equals variant.Id

					join product in Db.Products.AsNoTracking()
						on variant.ProductId equals product.Id

					join category in Db.Categories.AsNoTracking()
						on product.CategoryId equals category.Id

					join txn in Db.StockTransactions.AsNoTracking()
						on stock.Id equals txn.StockId into transactions

					let totalIn =
						transactions
							.Where(t => t.TransactionType == "IN")
							.Sum(t => (decimal?)t.Quantity) ?? 0

					let totalOut =
						transactions
							.Where(t => t.TransactionType == "OUT")
							.Sum(t => (decimal?)t.Quantity) ?? 0

					let adjust =
						transactions
							.Where(t => t.TransactionType == "ADJUST")
							.Sum(t => (decimal?)t.Quantity) ?? 0

					let currentStock = totalIn - totalOut + adjust

					select new CurrentStock
					{
						Id = stock.Id,

						ItemName = $"({category.Name}) {product.Name}-{variant.VariantName}",
						Category = stock.ItemType,

						Unit = stock.Unit,
						MinStockLevel = stock.MinStockLevel??0,
						CurrentQuantity = currentStock,

						Status =
							currentStock <= 0
								? "OutOfStock"
								: currentStock <= stock.MinStockLevel
									? "LowStock"
									: "InStock"
					}
				).ToListAsync();

			return result;
		}

	}

}
