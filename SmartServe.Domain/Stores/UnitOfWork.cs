using SmartServe.EFCore.Db;

namespace SmartServe.Domain.Stores
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly SmartServeDbContext _db;

		public UnitOfWork(SmartServeDbContext db)
		{
			_db = db;
		}

		public async Task BeginAsync()
		{
			await _db.Database.BeginTransactionAsync();
		}

		public async Task CommitAsync()
		{
			await _db.SaveChangesAsync();
			await _db.Database.CommitTransactionAsync();
		}

		public async Task RollbackAsync()
		{
			await _db.Database.RollbackTransactionAsync();
		}
	}

}
