using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blazet.Server
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServer(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }
    }
}
