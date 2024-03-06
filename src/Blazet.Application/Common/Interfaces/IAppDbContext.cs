using Blazet.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blazet.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Order> Orders { get; set; } //Consider.
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
