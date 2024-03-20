using Blazet.Application.Common.Interfaces;
using Blazet.Application.Orders.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blazet.Application.Orders.Queries
{
    public record OrderDtoItemListQuery : IRequest<List<OrderDto>>;

    public class OrderDtoItemListDtoQueryHandler : IRequestHandler<OrderDtoItemListQuery, List<OrderDto>>
    {
        private readonly IAppDbContext _appDbContext;

        public OrderDtoItemListDtoQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<OrderDto>> Handle(OrderDtoItemListQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Orders.Select(x => new OrderDto() {
                Id = x.Id,
                InternalNumber = x.InternalNumber, 
                Price = x.Price, 
                Quantity = x.Quantity}).ToListAsync();
        }
    }
}