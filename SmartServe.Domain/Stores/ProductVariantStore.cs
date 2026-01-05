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

		public async Task<IEnumerable<ProductVariant>> SaveBulkAsync(IEnumerable<ProductVariant> incoming)
		{
			var duplicate = incoming
				.GroupBy(x => x.VariantName.Trim().ToLower())
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
					if (variant.VariantId == 0)
					{
						Add(variant);
					}
					else
					{
						var tracked = Db.ProductVariants.Local
							.FirstOrDefault(x => x.VariantId == variant.VariantId);

						if (tracked == null)
						{
							tracked = new ProductVariant
							{
								VariantId = variant.VariantId
							};

							Attach(tracked);
						}

						// 🔑 Automatic diff, no property listing
						Db.Entry(tracked).CurrentValues.SetValues(variant);
					}
				}
				await SaveAsync();
				await _uow.CommitAsync();
				return incoming;
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
				.FirstOrDefault(x => x.VariantId == productVariantId);

			if (tracked == null)
			{
				tracked = new ProductVariant
				{
					VariantId = productVariantId
				};

				Attach(tracked);
			}
			Remove(tracked);
			await SaveAsync();
		}


	}
}
