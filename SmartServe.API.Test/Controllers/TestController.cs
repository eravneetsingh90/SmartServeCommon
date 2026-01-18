using Microsoft.AspNetCore.Mvc;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.API.Test.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		private readonly ProductStore _product;
		private readonly UserStore _user;
		private readonly RestaurantTableStore _tableStore;
		private readonly ILogger<TestController> _logger;

		public TestController(
			ILogger<TestController> logger,
			ProductStore product,
			UserStore user,
			RestaurantTableStore tableStore)
		{
			_logger = logger;
			_product = product;
			_user = user;
			_tableStore = tableStore;
		}

		[HttpGet]
		[Route("get-all-product")]
		public async Task<IEnumerable<ProductEntity>> GetAllProduct()
		{
			var products = await _product.GetAllAsync();
			return products;
		}

		[HttpGet]
		[Route("get-all-user")]
		public async Task<IEnumerable<UserEntity>> GetAllUser()
		{
			var users = await _user.GetAllAsync();
			return users;
		}

		[HttpGet]
		[Route("get-active-user")]
		public async Task<UserEntity> GetActiveUser(string username)
		{
			var user = await _user.GetActiveUserByUsernameAsync(username);
			return user;
		}

		// New test endpoint: returns table view DTOs produced by RestaurantTableStore.GetTablesForViewAsync
		[HttpGet]
		[Route("get-tables-view")]
		public async Task<IEnumerable<GetTableView>> GetTablesView()
		{
			var tables = await _tableStore.GetTablesForViewAsync();
			return tables;
		}
	}
}
