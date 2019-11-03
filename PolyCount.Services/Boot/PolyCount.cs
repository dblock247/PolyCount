using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace PolyCount.Services.Boot
{
    public class PolyCount : Default
    {
        public override IConfigurationBuilder BuildConfiguration(IConfigurationBuilder configuration)
        {
            configuration = base.BuildConfiguration(configuration)
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetEntryAssembly());

            return configuration;
        }

        public override IServiceCollection ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services = base.ConfigureServices(services, configuration);

            services.TryAddSingleton<IConfiguration>(configuration);

            return services;
        }
    }
}