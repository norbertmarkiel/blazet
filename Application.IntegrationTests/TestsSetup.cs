using Blazet.Application;
using Blazet.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Respawn;

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
            await Console.Out.WriteLineAsync(   );
    }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }
        public static async Task ResetState()
        {
            await Console.Out.WriteLineAsync();
            var tmp = _configuration;
            await _checkpoint.ResetAsync(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }
    }
}
