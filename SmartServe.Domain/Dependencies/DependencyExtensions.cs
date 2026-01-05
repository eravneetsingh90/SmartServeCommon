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
			
			//services
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IStockService, StockService>();
			services.AddSingleton<ICatalogService, CatalogService>();
			services.AddSingleton<IBillingService, BillingService>();
			services.AddScoped<IProductService, ProductService>();

			//stores
			services.AddScoped<IProductStore, ProductStore>();
			services.AddScoped<IProductVariantStore, ProductVariantStore>();
			services.AddScoped<UserStore>();
			services.AddScoped<IRestaurantTableStore, RestaurantTableStore>();
			services.AddScoped<ICategoryStore, CategoryStore>();
			services.AddScoped<IOrderItemStore, OrderItemStore>();
			services.AddScoped<IOrderStore, OrderStore>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<ITableStatusStore, TableStatusStore>();
			services.AddScoped<IPaymentStore, PaymentStore>();
			services.AddScoped<IBrandStore, BrandStore>();
			services.AddScoped<IStockStore, StockStore>();
			services.AddScoped<IIngredientStore, IngredientStore>();
			return services;
		}
	}
}


