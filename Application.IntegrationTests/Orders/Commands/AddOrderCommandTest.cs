using Blazet.Application.Orders.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Orders.Commands
{
    public class AddOrderCommandTest
    {

        private static IServiceScopeFactory _scopeFactory;

        [Test]
        public async Task AddOrder()
        {
            var command = new AddOrderCommand(12, 1);

            await SendAsync(command);
        }


        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
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
