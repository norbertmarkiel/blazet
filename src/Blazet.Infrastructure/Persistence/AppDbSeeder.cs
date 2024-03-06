using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
                _logger.LogError(ex, "An error occurred while initialising the database");
                throw;
            }
        }
    }
}
