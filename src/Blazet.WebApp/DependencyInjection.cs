using MudBlazor;
using MudBlazor.Services;

namespace Blazet.WebApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApp(this IServiceCollection services, IConfiguration config)
        {
            services.AddMudServices();
            return services;
        }
    }
}
