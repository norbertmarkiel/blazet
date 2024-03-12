using Blazet.Application.Common.Interfaces;
using Blazet.Domain.Orders.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blazet.Application.Orders.Commands
{
    public record UpdateOrderCommand(Guid Id, decimal Price, int Quantity) : IRequest;
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public UpdateOrderCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));
            if (order is null)
            {
                throw new OrderNotFoundException();
            }
            order.Update(request.Quantity, request.Price);
            _appDbContext.Orders.Update(order);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
