using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class ProductStore : BaseStore<Product>, IProductStore
	{
		private readonly IUnitOfWork _uow;
		public ProductStore(SmartServeDbContext db, IUnitOfWork uow) : base(db) { _uow = uow; }

		public async Task<List<Product>> GetByCategoryIdAsync(int categoryId)
		{
			return await Set
				.AsNoTracking()
				.Where(p => p.CategoryId == categoryId)
				.OrderBy(p => p.DisplayOrder)
				.ToListAsync();
		}

		public async Task SaveBulkAsync(IEnumerable<Product> products)
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
					if (product.ProductId == 0)
					{
						Add(product);
					}
					else
					{
						var tracked = Db.Products.Local
							.FirstOrDefault(x => x.ProductId == product.ProductId);

						if (tracked == null)
						{
							tracked = new Product
							{
								ProductId = product.ProductId
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
				.FirstOrDefault(x => x.ProductId == id);

			if (tracked == null)
			{
				tracked = new Product
				{
					ProductId = id
				};

				Attach(tracked);
			}
			Remove(tracked);
			await SaveAsync();
		}
	}

}
