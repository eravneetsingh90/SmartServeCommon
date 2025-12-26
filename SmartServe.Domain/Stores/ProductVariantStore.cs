using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class ProductVariantStore : BaseStore<ProductVariant>, IProductVariantStore
	{
		private readonly IUnitOfWork _uow;
		public ProductVariantStore(SmartServeDbContext db, IUnitOfWork uow) : base(db) { _uow = uow; }

		public async Task<List<ProductVariant>> GetProductsVariantByProductAsync(int productId)
		{
			return await _db.ProductVariants
				.AsNoTracking()
				.Where(p => p.ProductId == productId)
				.OrderBy(p => p.DisplayOrder)
				.ToListAsync();
		}

		public async Task SaveBulkProductVariantsAsync(IEnumerable<ProductVariant> productvariants)
		{
			var duplicate = productvariants
				.GroupBy(x => x.Name.Trim().ToLower())
				.Any(g => g.Count() > 1);

			if (duplicate)
			{
				throw new InvalidOperationException(
					"Duplicate variant names are not allowed for the same product.");
			}

			await _uow.BeginAsync();

			try
			{
				foreach (var variant in productvariants)
				{
					if (variant.ProductVariantId == 0)
						await AddAsync(variant);
					else
						await UpdateAsync(variant);
				}
				await _uow.CommitAsync();
			}
			catch
			{
				await _uow.RollbackAsync();
				throw;
			}

			
		}
	}
}
