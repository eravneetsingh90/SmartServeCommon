using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Constants;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class StockStore : BaseStore<Stock>, IStockStore
	{

		public StockStore(SmartServeDbContext db) : base(db)
		{
		}

		public async Task<List<Stock>> GetStockAsync()
		{
			return await Set
				.Where(x => x.IsActive == true)
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<List<Stock>> GetStockAsync(string itemType)
		{
			return await Set
				.Where(x => x.ItemType == itemType)
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<Stock?> GetStockAsync(string itemType, int referenceId)
		{
			return await Set
				.FirstOrDefaultAsync(x =>
					x.ItemType == itemType &&
					x.ReferenceId == referenceId);
		}
		public async Task AddStockAsync(Stock stockItem)
		{
			Set.Add(stockItem);
			await SaveAsync();
		}

		public async Task UpdateStockAsync(Stock stockItem)
		{
			Set.Update(stockItem);
			await SaveAsync();
		}

		public async Task<List<CurrentStockDto>> GetCurrentStockAsync()
		{
			var query =
				from si in Set
				where si.IsActive == true

				join st in Db.StockTransactions
					on si.Id equals st.Id into txnGroup

				// VARIANT JOIN
				join pv in Db.ProductVariants
					on new { RefId = si.ReferenceId, Type = si.ItemType }
					equals new { RefId = pv.VariantId, Type = "SEALED" }
					into variantJoin
				from variant in variantJoin.DefaultIfEmpty()

				join p in Db.Products
					on variant.ProductId equals p.ProductId
					into productJoin
				from product in productJoin.DefaultIfEmpty()

				join b in Db.Brands
					on variant.BrandId equals b.BrandId
					into brandJoin
				from brand in brandJoin.DefaultIfEmpty()

					// INGREDIENT JOIN
				join ing in Db.Ingredients
					on new { RefId = si.ReferenceId, Type = si.ItemType }
					equals new { RefId = ing.IngredientId, Type = "INGREDIENT" }
					into ingredientJoin
				from ingredient in ingredientJoin.DefaultIfEmpty()

				select new CurrentStockDto
				{
					Id = si.Id,
					ItemType = si.ItemType,
					ReferenceId = si.ReferenceId,
					Unit = si.Unit,
					MinStockLevel = si.MinStockLevel ?? 0,

					ItemName =
						si.ItemType == "VARIANT"
							? (brand != null
								? $"{product.Name} - {variant.VariantName} ({brand.Name})"
								: $"{product.Name} - {variant.VariantName}")
							: ingredient.Name,

					CurrentQuantity =
						txnGroup.Sum(t =>
							t.TransactionType == "IN" ? t.Quantity :
							t.TransactionType == "OUT" ? -t.Quantity :
							t.Quantity)
				};

			return await query
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<decimal> GetCurrentStockQuantityAsync(int stockItemId)
		{
			var qty = await Db.StockTransactions
				.Where(x => x.Id == stockItemId)
				.SumAsync(x =>
					x.TransactionType == StockTxnType.IN ? x.Quantity :
					x.TransactionType == StockTxnType.OUT ? -x.Quantity :
					x.Quantity);

			return qty;
		}

	}

}
