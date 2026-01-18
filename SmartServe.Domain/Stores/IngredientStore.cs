using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class IngredientStore : BaseStore<IngredientEntity>, IIngredientStore
	{
		public IngredientStore(SmartServeDbContext db) : base(db)
		{
		}
		
	}

}
