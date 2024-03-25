using Blazet.Application.Identity;
using Blazet.Application.Identity.DTOs;
using Blazet.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blazet.Infrastructure.Services.Identity
{
    internal class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(
            IServiceScopeFactory scopeFactory)
        {
            var scope = scopeFactory.CreateScope();
            _userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            DataSource = new List<AppUserDto>();
        }

        public List<AppUserDto> DataSource { get; private set; }

        public event Action? OnChange;

        public void Initialize()
        {
            throw new NotImplementedException();
        }



        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
