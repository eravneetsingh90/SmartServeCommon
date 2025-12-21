using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class CategoryStore : BaseStore<Category>
	{
		public CategoryStore(SmartServeDbContext db) : base(db) { }

		public async Task<List<Category>> GetActiveCategoriesAsync()
		{
			return await _db.Categories
				.Where(c => c.IsActive == true)
				.OrderBy(c => c.Name)
				.ToListAsync();
		}
	}

}
