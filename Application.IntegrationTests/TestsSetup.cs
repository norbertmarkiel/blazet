using Blazet.Application;
using Blazet.Infrastructure;
using Blazet.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Respawn;
using Respawn.Graph;

namespace Application.IntegrationTests
{
    [SetUpFixture]
    public class TestsSetup
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Respawner _checkpoint;


        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();

            var services = new ServiceCollection();


            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "WebApp"));

            services.AddInfrastructure(_configuration)
                .AddApplication();
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
            _checkpoint = await Respawner.CreateAsync(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"),
                new RespawnerOptions
                {
                    TablesToIgnore = new Table[] { "__EFMigrationsHistory" }
                });

            EnsureDatabase();

            await Console.Out.WriteLineAsync(   );
    }
        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<AppDbContext>();

            context.Database.Migrate();
        }
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }
        public static async Task ResetState()
        {
            await _checkpoint.ResetAsync(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }


        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            var x = mediator.Send(request);
            return await mediator.Send(request);
        }

        public static async Task SendAsync(IRequest request)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            await mediator.Send(request);
        }
    }
}
