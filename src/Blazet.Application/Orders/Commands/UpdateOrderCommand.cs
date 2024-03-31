using Blazet.Application.Common.Interfaces;
using Blazet.Domain.Orders.Entities;
using Blazet.Domain.Orders.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blazet.Application.Orders.Commands
{
    public record UpdateOrderCommand(Guid Id, decimal Price, int Quantity) : IRequest;
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IValidator<Order> _validator;

        public UpdateOrderCommandHandler(IAppDbContext appDbContext,
            IValidator<Order> validator
        )
        {
            _appDbContext = appDbContext;
            _validator = validator;
        }
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));
            if (order is null)
            {
                throw new OrderNotFoundException();
            }
            order.Update(request.Quantity, request.Price);

            var validationResult = await _validator.ValidateAsync(order);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(",", validationResult.Errors);
                throw new OrderInvalidDataException(errorMessages);
            }

            _appDbContext.Orders.Update(order);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
