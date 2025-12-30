using Microsoft.Extensions.DependencyInjection;
using SmartServe.Domain.Mapping;
using SmartServe.Domain.Services;
using SmartServe.Domain.Stores;

namespace SmartServe.Domain.Dependencies
{
	public static class DependencyExtensions
	{
		public static IServiceCollection UseDomain(
			this IServiceCollection services)
		{
			//mapping profiles
			services.AddAutoMapper(typeof(MappingProfile));
			//services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			//services
			services.AddScoped<IAuthService,AuthService>();
			services.AddSingleton<ICatalogService, CatalogService>();
			services.AddSingleton<IBillingService, BillingService>();

			//stores
			services.AddScoped<IProductStore, ProductStore>();
			services.AddScoped<IProductVariantStore, ProductVariantStore>();
			services.AddScoped<UserStore>();
			services.AddScoped<IRestaurantTableStore,RestaurantTableStore>();
			services.AddScoped<ICategoryStore, CategoryStore>();
			services.AddScoped<IOrderItemStore,OrderItemStore>();
			services.AddScoped<IOrderStore,OrderStore>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<ITableStatusStore, TableStatusStore>();
			return services;
		}
	}
}

