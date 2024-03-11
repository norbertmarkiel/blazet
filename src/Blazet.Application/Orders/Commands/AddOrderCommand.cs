using Blazet.Application.Common.Interfaces;
using Blazet.Domain.Orders.Entities;
using MediatR;

namespace Blazet.Application.Orders.Commands
{
    public record AddOrderCommand(decimal Price, int Quantity) : IRequest;
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public AddOrderCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order("404", request.Quantity, request.Price);

            _appDbContext.Orders.Add(order);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
