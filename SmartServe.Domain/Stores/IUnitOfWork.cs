using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Domain.Stores
{
	public interface IUnitOfWork
	{
		Task BeginAsync();
		Task CommitAsync();
		Task RollbackAsync();
	}

}
