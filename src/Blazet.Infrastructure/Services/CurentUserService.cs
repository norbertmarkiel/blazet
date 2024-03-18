using Blazet.Application.Common.Interfaces;

namespace Blazet.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
    }
}
