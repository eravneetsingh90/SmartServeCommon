using Microsoft.Extensions.DependencyInjection;
using SmartServe.Domain.Services;
using SmartServe.Domain.Stores;

namespace SmartServe.Domain.Dependencies
{
	public static class DependencyExtensions
	{
		public static IServiceCollection UseDomain(
			this IServiceCollection services)
		{
			//services
			services.AddScoped<AuthService>();
			services.AddSingleton<ICatalogService, CatalogService>();

			//stores
			services.AddScoped<ProductStore>();
			services.AddScoped<ProductVariantStore>();
			services.AddScoped<UserStore>();
			services.AddScoped<RestaurantTableStore>();
			services.AddScoped<CategoryStore>();
			services.AddScoped<OrderItemStore>();
			services.AddScoped<OrderStore>();

			return services;
		}
	}
}

