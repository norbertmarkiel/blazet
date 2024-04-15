using Blazet.Application.Orders.Validators;
using Blazet.Application.Pipeline;
using Blazet.Domain.Orders.Entities;
using FluentValidation;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blazet.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(RequestExceptionProcessorBehavior<,>));
                config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            });
            services.AddValidators();
            return services;
        }
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Order>, OrderValidator>();
            return services;
        }
    }
}