using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PolyCount.Services.Interfaces;

namespace PolyCount.Services.Boot
{
    public class Default : IBootable
    {
        public virtual IConfigurationBuilder BuildConfiguration(IConfigurationBuilder configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", false, true)
                .AddJsonFile($"appSettings.{environment}.json", true, true);

            return configuration;
        }

        public virtual IServiceCollection ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.TryAddSingleton<IConfiguration>(configuration);

            return services;
        }
    }
}