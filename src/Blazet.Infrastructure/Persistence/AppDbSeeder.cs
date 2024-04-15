using Blazet.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blazet.Infrastructure.Persistence
{
    public class AppDbSeeder
    {
        private readonly AppDbContext _context;
        public AppDbSeeder(AppDbContext context)
        {
            _context = context;
        }
        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer() /*|| _context.Database.IsNpgsql() || _context.Database.IsSqlite()*/)
                    await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //_logger.LogError(ex, "An error occurred while initialising the database");
                throw;
            }
        }
        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
                _context.ChangeTracker.Clear();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }

        private async Task TrySeedAsync()
        {
            if (!_context.Orders.Any())
            {
                _context.Orders.Add(new Order("1", 1, 1.00m));
                _context.Orders.Add(new Order("2", 2, 2.00m));
                _context.Orders.Add(new Order("3", 3, 3.00m));
                _context.Orders.Add(new Order("4", 4, 4.00m));
            }
                await _context.SaveChangesAsync();
            }
        }
}
