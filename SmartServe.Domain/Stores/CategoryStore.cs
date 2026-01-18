using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using System.Runtime.CompilerServices;

namespace SmartServe.Domain.Stores
{
	public class CategoryStore : BaseStore<CategoryEntity>, ICategoryStore
	{
		private readonly IUnitOfWork _uow;
		public CategoryStore(SmartServeDbContext db, IUnitOfWork uow) : base(db) 
		{
			_uow = uow;
		}

		public async Task<List<CategoryEntity>> GetActiveAsync()
		{
			return await Set
				.AsNoTracking()
				.Where(c => c.IsActive == true)
				.OrderBy(c => c.DisplayOrder)
				.ToListAsync();
		}

		public async Task<List<CategoryEntity>> GetAllAsync()
		{
			return await Set
				.AsNoTracking()
				.OrderBy(c => c.DisplayOrder)
				.ToListAsync();
		}

		public async Task SaveBulkAsync(IEnumerable<CategoryEntity> categories)
		{
			var duplicateNames = categories
				.Where(c => !string.IsNullOrWhiteSpace(c.Name))
				.GroupBy(c => c.Name.Trim().ToLower())
				.Where(g => g.Count() > 1)
				.Select(g => g.Key)
				.ToList();

			if (duplicateNames.Any())
				throw new DuplicateWaitObjectException(
					"Duplicate category names are not allowed.");

			await _uow.BeginAsync();

			try
			{
				foreach (var category in categories)
				{
					if (category.Id == 0)
						Add(category);
					else
					{
						var tracked = Db.Categories.Local
							.FirstOrDefault(x => x.Id == category.Id);

						if (tracked == null)
						{
							tracked = new CategoryEntity
							{
								Id = category.Id
							};

							Attach(tracked);
						}
						Db.Entry(tracked).CurrentValues.SetValues(category);
					}
				}

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
			var tracked = Db.Categories.Local
				.FirstOrDefault(x => x.Id == id);

			if (tracked == null)
			{
				tracked = new CategoryEntity
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
