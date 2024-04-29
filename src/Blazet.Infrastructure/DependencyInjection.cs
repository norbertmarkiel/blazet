using Blazet.Application.Common.Interfaces;
using Blazet.Application.Common.Interfaces.Identity;
using Blazet.Application.Identity;
using Blazet.Domain.Identity;
using Blazet.Infrastructure.Configuration;
using Blazet.Infrastructure.Constants;
using Blazet.Infrastructure.Constants.User;
using Blazet.Infrastructure.Persistence;
using Blazet.Infrastructure.Security;
using Blazet.Infrastructure.Services;
using Blazet.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;


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
                options.UseInMemoryDatabase("BlazorDb");
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
    private static IServiceCollection AddAuthenticationService(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            var identitySettings = configuration.GetRequiredSection(IdentitySettings.Key).Get<IdentitySettings>();


            // Password settings
            options.Password.RequireDigit = identitySettings!.RequireDigit;
            options.Password.RequiredLength = identitySettings.RequiredLength;
            options.Password.RequireNonAlphanumeric = identitySettings.RequireNonAlphanumeric;
            options.Password.RequireUppercase = identitySettings.RequireUpperCase;
            options.Password.RequireLowercase = identitySettings.RequireLowerCase;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identitySettings.DefaultLockoutTimeSpan);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.AllowedForNewUsers = true;

            // Default SignIn settings.
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            // User settings
            options.User.RequireUniqueEmail = true;
        });

        services.AddScoped<IIdentityService, IdentityService>()
            .AddAuthorizationCore(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireUserName(UserName.Administrator));
                // Here I stored necessary permissions/roles in a constant
                foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c =>
                             c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
                {
                    var propertyValue = prop.GetValue(null);
                    if (propertyValue is not null)
                        options.AddPolicy((string)propertyValue,
                            policy => policy.RequireClaim(AppClaimTypes.Permission, (string)propertyValue));
                }
            })
            .AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("@nwq!:[Pm@KNm{:?nNRJyW;D}-XN:7Q*zZ%x}y3pB9=FLZUL74")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Headers.Authorization;
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/signalRHub")) // TODO: move in server?
                            context.Token = accessToken.ToString().Substring(7);
                        return Task.CompletedTask;
                    }
                };
            });
        services.ConfigureApplicationCookie(options => { options.LoginPath = "/pages/authentication/login"; });
        services.AddSingleton<UserService>()
            .AddSingleton<IUserService>(sp =>
            {
                var service = sp.GetRequiredService<UserService>();
                service.Initialize();
                return service;
            });
        return services;
    }
}
