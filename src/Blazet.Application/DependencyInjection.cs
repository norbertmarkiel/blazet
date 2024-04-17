using Blazet.Application.Orders.Commands;
using Blazet.Application.Orders.Validators;
using Blazet.Application.Pipeline;
using Blazet.Application.Pipeline.Preprocessors;
using Blazet.Domain.Orders.Entities;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Blazet.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //services.AddMediatR(typeof(Assembly1), typeof(Assembly2));
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(ValidationPreProcessor<>));
                config.AddOpenBehavior(typeof(RequestExceptionProcessorBehavior<,>));
                config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            });
            services.AddValidators();
            return services;
        }
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Order>, OrderValidator>();
            services.AddScoped<IValidator<AddOrderCommand>, AddOrderCommandValidator>();

            return services;
        }
    }
}