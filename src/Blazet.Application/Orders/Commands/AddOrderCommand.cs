using Blazet.Application.Common.Interfaces;
using Blazet.Application.Common.Models;
using Blazet.Domain.Orders.Entities;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace Blazet.Application.Orders.Commands
{
    public record AddOrderCommand(decimal Price, int Quantity) : IRequest<Result<Guid>>;
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Result<Guid>>
    {
        private readonly IAppDbContext _appDbContext;

        public AddOrderCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Result<Guid>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order("404", request.Quantity, request.Price);

        _appDbContext.Orders.Add(order);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        
            return await Result<Guid>.SuccessAsync(order.Id);
        } 
    }

}
