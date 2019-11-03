using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PolyCount.Services.Interfaces;

namespace PolyCount.Services.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddPolyCountServices(this IServiceCollection services)
        {
            services.TryAddScoped<IGraph, Graph>();

            return services;
        }

        public static IServiceCollection AddBootstrap<T>(this IServiceCollection services) where T : IBootable, new()
        {
            var bootstrap = Bootstrap.Load<T>(services);

            return bootstrap.Services;
        }
    }
}