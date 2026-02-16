using Microsoft.Extensions.DependencyInjection;

namespace SmartServe.Resources.Dependencies
{
    public static class DependencyExtensions
    {
        public static IServiceCollection UseResource(
            this IServiceCollection services)
        {
            //services.AddScoped<ITenantProvider, TenantProvider>();

            return services;
        }
    }
}
