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
		private readonly UserStore _user;
		private readonly ILogger<TestController> _logger;

		public TestController(ILogger<TestController> logger, ProductStore product, UserStore user)
		{
			_logger = logger;
			_product = product;
			_user = user;
		}

		[HttpGet]
		[Route("get-all-product")]
		public async Task<IEnumerable<Product>> GetAllProduct()
		{
			var products = await _product.GetAllAsync();
			return products;
		}
		[HttpGet]
		[Route("get-all-user")]
		public async Task<IEnumerable<User>> GetAllUser()
		{
			var users = await _user.GetAllAsync();
			return users;

		}

		[HttpGet]
		[Route("get-active-user")]
		public async Task<User> GetActiveUser(string username)
		{
			var user = await _user.GetActiveUserByUsernameAsync(username);
			return user;

		}
	}
}
