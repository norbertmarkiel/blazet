using Blazet.Application.Common.Interfaces;
using Blazet.Domain.Orders.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Blazet.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext, IAppDbContext
    {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //builder.ApplyGlobalFilters<ISoftDelete>(s => s.Deleted == null);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }


    }
}
