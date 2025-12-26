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
			services.AddScoped<IProductStore, ProductStore>();
			services.AddScoped<IProductVariantStore, ProductVariantStore>();
			services.AddScoped<UserStore>();
			services.AddScoped<RestaurantTableStore>();
			services.AddScoped<ICategoryStore, CategoryStore>();
			services.AddScoped<OrderItemStore>();
			services.AddScoped<OrderStore>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}

