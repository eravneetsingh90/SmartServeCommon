using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using SmartServe.Resources.Provider;

namespace SmartServe.Domain.Stores
{
	public class ProductStore : BaseStore<ProductEntity>, IProductStore
	{
		private readonly IUnitOfWork _uow;
		public ProductStore(SmartServeDbContext db, ITenantProvider tenantProvider, IUnitOfWork uow) : base(db, tenantProvider) { _uow = uow; }

		public async Task<List<ProductEntity>> GetByCategoryIdAsync(int categoryId)
		{
			return await Set
				.AsNoTracking()
				.Where(p => p.CategoryId == categoryId)
				.OrderBy(p => p.DisplayOrder)
				.ToListAsync();
		}

		public async Task SaveBulkAsync(IEnumerable<ProductEntity> products)
		{
			var duplicateNames = products
				.Where(p => !string.IsNullOrWhiteSpace(p.Name))
				.GroupBy(p => p.Name.Trim().ToLower())
				.Where(g => g.Count() > 1)
				.Select(g => g.Key)
				.ToList();

			if (duplicateNames.Any())
				throw new DuplicateWaitObjectException(
					"Duplicate product names are not allowed within the same category.");

			await _uow.BeginAsync();
			try
			{
				foreach (var product in products)
				{
					if (product.Id == 0)
					{
						Add(product);
					}
					else
					{
						var tracked = Db.Products.Local
							.FirstOrDefault(x => x.Id == product.Id);

						if (tracked == null)
						{
							tracked = new ProductEntity
							{
								Id = product.Id
							};

							Attach(tracked);
						}
						Db.Entry(tracked).CurrentValues.SetValues(product);
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

		public async Task DeleteAsync(int id)
		{
			var tracked = Db.Products.Local
				.FirstOrDefault(x => x.Id == id);

			if (tracked == null)
			{
				tracked = new ProductEntity
				{
					Id = id
				};

				Attach(tracked);
			}
			Remove(tracked);
			await SaveAsync();
		}
	}

}
