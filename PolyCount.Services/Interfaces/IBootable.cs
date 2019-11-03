using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PolyCount.Services.Interfaces
{
    public interface IBootable
    {
        IConfigurationBuilder BuildConfiguration(IConfigurationBuilder configuration);
        IServiceCollection ConfigureServices(IServiceCollection services, IConfigurationRoot configuration);
    }
}