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

			//stores
			services.AddScoped<ProductStore>();
			services.AddScoped<UserStore>();

			return services;
		}
	}
}

