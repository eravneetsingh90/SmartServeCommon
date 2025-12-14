using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Entities;

namespace SmartServe.API.Test.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		private readonly ProductStore _product;
		private readonly ILogger<TestController> _logger;

		public TestController(ILogger<TestController> logger, ProductStore product)
		{
			_logger = logger;
			_product = product;
		}

		[HttpGet(Name = "get-all-product")]
		public async Task<IEnumerable<Product>> GetAllProduct()
		{
			var products = await _product.GetAllAsync();
			return products;
		}
	}
}
