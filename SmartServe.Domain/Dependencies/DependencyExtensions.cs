using Microsoft.Extensions.DependencyInjection;
using SmartServe.Domain.Stores;

namespace SmartServe.Domain.Dependencies
{
	public static class DependencyExtensions
	{
		public static IServiceCollection UseSmartServeStores(
			this IServiceCollection services)
		{
			services.AddScoped<ProductStore>();
			return services;
		}
	}
}

