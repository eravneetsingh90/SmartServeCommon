using SmartServe.Domain.Models;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using SmartServe.Resources.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Domain.Stores
{
	public class StockTransactionStore : BaseStore<StockTransactionEntity>, IStockTransactionStore
	{

		public StockTransactionStore(SmartServeDbContext db, ITenantProvider tenantProvider) : base(db, tenantProvider)
		{
		}

	}

}
