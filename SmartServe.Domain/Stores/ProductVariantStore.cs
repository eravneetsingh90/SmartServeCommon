using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using SmartServe.Resources.Provider;

namespace SmartServe.Domain.Stores
{
	public class ProductVariantStore
		: BaseStore<ProductVariantEntity>, IProductVariantStore
	{
		private readonly IUnitOfWork _uow;

		public ProductVariantStore(
			SmartServeDbContext db,
            ITenantProvider tenantProvider,
            IUnitOfWork uow) : base(db, tenantProvider)
		{
			_uow = uow;
		}

		public async Task<List<ProductVariantEntity>> GetByProductIdAsync(int productId)
		{
			return await Set
				.AsNoTracking()
				.Where(v => v.ProductId == productId)
				.OrderBy(v => v.DisplayOrder)
				.ToListAsync();
		}

		public async Task<List<ProductVariantEntity>> GetByBrandIdAsync(int brandId)
		{
			return await Set
				.AsNoTracking()
				.Include(v => v.Product)
				.Where(v => v.BrandId == brandId)
				.OrderBy(v => v.DisplayOrder)
				.ToListAsync();
		}

		public async Task<IEnumerable<ProductVariantEntity>> SaveBulkAsync(IEnumerable<ProductVariantEntity> incoming)
		{
			var duplicate = incoming
				.GroupBy(x => x.VariantName.Trim().ToLower())
				.Any(g => g.Count() > 1);
			if (duplicate)
			{
				throw new DuplicateWaitObjectException(
					"Duplicate variant names are not allowed for the same product.");
			}
			await _uow.BeginAsync();
			try
			{
				foreach (var variant in incoming)
				{
					if (variant.Id == 0)
					{
						Add(variant);
					}
					else
					{
						var tracked = Db.ProductVariants.Local
							.FirstOrDefault(x => x.Id == variant.Id);

						if (tracked == null)
						{
							tracked = new ProductVariantEntity
							{
								Id = variant.Id
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
				.FirstOrDefault(x => x.Id == productVariantId);

			if (tracked == null)
			{
				tracked = new ProductVariantEntity
				{
					Id = productVariantId
				};

				Attach(tracked);
			}
			Remove(tracked);
			await SaveAsync();
		}


	}
}
