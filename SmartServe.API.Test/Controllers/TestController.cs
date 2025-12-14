using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Entities;

namespace SmartServe.API.Test.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		private readonly SmartServeDbContext _db;
		private readonly ILogger<TestController> _logger;

		public TestController(ILogger<TestController> logger, SmartServeDbContext db)
		{
			_logger = logger;
			_db = db;
		}

		[HttpGet(Name = "get-all-product")]
		public async Task<IEnumerable<Product>> GetAllProduct()
		{
			var products = await _db.products.AsNoTracking().ToListAsync();
			return products;
		}
	}
}
