using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Blazet.Infrastructure.Persistence;
using Blazet.Infrastructure.Configuration;
using Blazet.Application.Common.Interfaces;
using Blazet.Infrastructure.Services;

namespace Blazet.Infrastructure;
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                IConfiguration configuration)
        {

        services
            .AddSettings(configuration)
        .AddDatabase(configuration);



            return services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    private static IServiceCollection AddSettings(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.Key))
            .AddSingleton(s => s.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        return services;
    }
    private static IServiceCollection AddDatabase(this IServiceCollection services,
      IConfiguration configuration)
    {
        //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>()
        //    .AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("BlazetDb");

                options.UseInMemoryDatabase("BlazorDashboardDb");
                options.EnableSensitiveDataLogging();
            });
        else
            services.AddDbContext<AppDbContext>((p, m) =>
            {
                var databaseSettings = p.GetRequiredService<IOptions<DatabaseSettings>>().Value;


                m.AddInterceptors(p.GetServices<ISaveChangesInterceptor>());
                m.UseDatabase(databaseSettings.DBProvider, databaseSettings.ConnectionString);
            });

        services.AddScoped<IDbContextFactory<AppDbContext>, BlazorContextFactory<AppDbContext>>();
        services.AddTransient<IAppDbContext>(provider =>
            provider.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext());
        services.AddScoped<AppDbSeeder>();

        return services;
    }

    private static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string dbProvider,
    string connectionString)
    {
                return builder.UseSqlServer(connectionString);
    }

}
