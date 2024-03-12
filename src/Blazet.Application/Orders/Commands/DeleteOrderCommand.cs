using Blazet.Application.Common.Interfaces;
using Blazet.Domain.Orders.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blazet.Application.Orders.Commands
{
    public record DeleteOrderCommand(Guid Id) : IRequest;
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public DeleteOrderCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));
            if (order is null)
            {
                throw new OrderNotFoundException();
            }
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
