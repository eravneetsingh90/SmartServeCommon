using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class CategoryStore : BaseStore<Category>, ICategoryStore
	{
		private readonly IUnitOfWork _uow;
		public CategoryStore(SmartServeDbContext db, IUnitOfWork uow) : base(db) 
		{
			_uow = uow;
		}

		public async Task<List<Category>> GetActiveCategoriesAsync()
		{
			return await _db.Categories
				.Where(c => c.IsActive == true)
				.OrderBy(c => c.DisplayOrder)
				.ToListAsync();
		}

		public async Task<List<Category>> GetAllCategoriesByOrderAsync()
		{
			return await _db.Categories
				.OrderBy(c => c.DisplayOrder)
				.ToListAsync();
		}

		public async Task SaveBulkCategoriesAsync(IEnumerable<Category> categories)
		{
			// 🔹 Business Rule: No duplicate names
			var duplicateNames = categories
				.Where(c => !string.IsNullOrWhiteSpace(c.Name))
				.GroupBy(c => c.Name.Trim().ToLower())
				.Where(g => g.Count() > 1)
				.Select(g => g.Key)
				.ToList();

			if (duplicateNames.Any())
				throw new InvalidOperationException(
					"Duplicate category names are not allowed.");

			await _uow.BeginAsync();

			try
			{
				foreach (var category in categories)
				{
					if (category.CategoryId == 0)
						await AddAsync(category);
					else
						await UpdateAsync(category);
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
