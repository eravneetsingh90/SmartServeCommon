using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class StockTransactionStore : BaseStore<StockTransactionEntity>, IStockTransactionStore
	{

		public StockTransactionStore(SmartServeDbContext db) : base(db)
		{
		}

	}

}
