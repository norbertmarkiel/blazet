using Blazet.Application.Identity.DTOs;

namespace Blazet.Application.Identity
{
    public interface IUserService
    {
        List<AppUserDto> DataSource { get; }
        event Action? OnChange;
        void Initialize();
        void Refresh();
    }
}
