using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartServe.EFCore.Db;

namespace SmartServe.EFCore.Dependencies

{
	public static class DependencyExtensions
	{
		public static IServiceCollection UseEFCore(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<SmartServeDbContext>(options =>
			{
				options.UseNpgsql(
					configuration.GetConnectionString("SmartServeDb")
				);
			});

			return services;
		}
	}
}
