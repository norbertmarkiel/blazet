using MediatR;

namespace Blazet.Application.Orders.Commands
{
    public record AddOrderCommand(decimal price, int Quantity) : IRequest;
}
