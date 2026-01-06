using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class IngredientStore : BaseStore<Ingredient>, IIngredientStore
	{
		public IngredientStore(SmartServeDbContext db) : base(db)
		{
		}
		
	}

}
