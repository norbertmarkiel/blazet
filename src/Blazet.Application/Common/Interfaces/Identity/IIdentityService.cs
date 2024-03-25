using Blazet.Application.Common.Models;
using Blazet.Application.Identity.DTOs;

namespace Blazet.Application.Common.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId, CancellationToken cancellation = default);
        Task<bool> IsInRoleAsync(string userId, string role, CancellationToken cancellation = default);
        Task<bool> AuthorizeAsync(string userId, string policyName, CancellationToken cancellation = default);
        Task<Result> DeleteUserAsync(string userId, CancellationToken cancellation = default);
        Task<IDictionary<string, string?>> FetchUsers(string roleName, CancellationToken cancellation = default);
        Task<AppUserDto> GetAppUserDto(string userId, CancellationToken cancellation = default);
        string GetUserName(string userId);
    }
}
