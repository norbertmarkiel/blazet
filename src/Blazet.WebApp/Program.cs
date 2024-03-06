using Blazet.Application;
using Blazet.Infrastructure;
using Blazet.Infrastructure.Persistence;
using Blazet.Server;
using Blazet.WebApp.Components;

namespace Blazet.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddServer(builder.Configuration)
                .AddWebApp(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else {
                using (var scope = app.Services.CreateScope())
                {
                    var initializer = scope.ServiceProvider.GetRequiredService<AppDbSeeder>();
                    //await initializer.InitialiseAsync();
                    //await initializer.SeedAsync();
                }
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
