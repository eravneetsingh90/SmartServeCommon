using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class ProductStore : BaseStore<Product>, IProductStore
	{
		private readonly IUnitOfWork _uow;
		public ProductStore(SmartServeDbContext db, IUnitOfWork uow) : base(db) { _uow = uow; }

		public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
		{
			return await _db.Products
				.AsNoTracking()
				.Where(p => p.CategoryId == categoryId)
				.OrderBy(p => p.DisplayOrder)
				.ToListAsync();
		}

		public async Task SaveBulkProductsAsync(IEnumerable<Product> products)
		{
			var duplicateNames = products
				.Where(p => !string.IsNullOrWhiteSpace(p.Name))
				.GroupBy(p => p.Name.Trim().ToLower())
				.Where(g => g.Count() > 1)
				.Select(g => g.Key)
				.ToList();

			if (duplicateNames.Any())
				throw new InvalidOperationException(
					"Duplicate product names are not allowed within the same category.");

			await _uow.BeginAsync();

			try
			{
				foreach (var product in products)
				{
					if (product.ProductId == 0)
						await AddAsync(product);
					else
						await UpdateAsync(product);
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
