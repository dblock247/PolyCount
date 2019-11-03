using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PolyCount.Services.Interfaces;

namespace PolyCount.Services
{
    public class Bootstrap
    {
        private IServiceCollection _services;
        public IServiceCollection Services => _services;

        private IConfiguration _configuration;
        public IConfiguration ConfigurationBuilder => _configuration;

        private Bootstrap() { }

        public static Bootstrap Load<T>(IServiceCollection services = null) where T: IBootable, new()
        {
            var bootLoader = new T();
            var builder = new ConfigurationBuilder();
            var configuration = bootLoader.BuildConfiguration(builder)
                .Build();

            services ??= new ServiceCollection();
            services = bootLoader.ConfigureServices(services, configuration);

            return new Bootstrap
            {
                _configuration = builder.Build(),
                _services =  bootLoader.ConfigureServices(services, configuration)
            };
        }

        public IServiceProvider Build()
        {
            return _services.BuildServiceProvider();
        }
    }
}