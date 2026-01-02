using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class ProductVariantStore
		: BaseStore<ProductVariant>, IProductVariantStore
	{
		private readonly IUnitOfWork _uow;

		public ProductVariantStore(
			SmartServeDbContext db,
			IUnitOfWork uow) : base(db)
		{
			_uow = uow;
		}

		public async Task<List<ProductVariant>> GetByProductIdAsync(int productId)
		{
			return await Set
				.AsNoTracking()
				.Where(v => v.ProductId == productId)
				.OrderBy(v => v.DisplayOrder)
				.ToListAsync();
		}

		public async Task SaveBulkAsync(IEnumerable<ProductVariant> incoming)
		{
			var duplicate = incoming
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
				foreach (var variant in incoming)
				{
					if (variant.ProductVariantId == 0)
					{
						Add(variant);
					}
					else
					{
						var tracked = Db.ProductVariants.Local
							.FirstOrDefault(x => x.ProductVariantId == variant.ProductVariantId);

						if (tracked == null)
						{
							tracked = new ProductVariant
							{
								ProductVariantId = variant.ProductVariantId
							};

							Attach(tracked);
						}

						// 🔑 Automatic diff, no property listing
						Db.Entry(tracked).CurrentValues.SetValues(variant);
					}
				}
				await SaveAsync();
				await _uow.CommitAsync();
			}
			catch
			{
				await _uow.RollbackAsync();
				throw;
			}
		}


		public async Task DeleteAsync(int productVariantId)
		{
			var tracked = Db.ProductVariants.Local
				.FirstOrDefault(x => x.ProductVariantId == productVariantId);

			if (tracked == null)
			{
				tracked = new ProductVariant
				{
					ProductVariantId = productVariantId
				};

				Attach(tracked);
			}
			Remove(tracked);
			await SaveAsync();
		}


	}
}
