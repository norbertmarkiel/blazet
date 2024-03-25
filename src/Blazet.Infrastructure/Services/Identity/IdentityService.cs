using Blazet.Application.Common.Exceptions;
using Blazet.Application.Common.Interfaces.Identity;
using Blazet.Application.Common.Models;
using Blazet.Application.Identity.DTOs;
using Blazet.Domain.Identity;
using Blazet.Infrastructure.Extensions;
using Blazet.Infrastructure.Extensions.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
namespace Blazet.Infrastructure.Services.Identity
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStringLocalizer<IdentityService> _localizer;
        private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        //private readonly IAppCache _cache;


        //private LazyCacheEntryOptions Options =>
        //    new LazyCacheEntryOptions().SetAbsoluteExpiration(RefreshInterval, ExpirationMode.LazyExpiration);

        public async Task<bool> AuthorizeAsync(string userId, string policyName, CancellationToken cancellation = default)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId, cancellation) ??
                       throw new NotFoundException(_localizer["User Not Found."]);
            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
            var result = await _authorizationService.AuthorizeAsync(principal, policyName);
            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId, CancellationToken cancellation = default)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId, cancellation) ??
                       throw new NotFoundException(_localizer["User Not Found."]);
            var result = await _userManager.DeleteAsync(user);
            return result.ToApplicationResult();
        }

        public async Task<IDictionary<string, string?>> FetchUsers(string roleName, CancellationToken cancellation = default)
        {
            var result = await _userManager.Users
                .Where(x => x.UserRoles.Any(y => y.Role.Name == roleName))
                .Include(x => x.UserRoles)
                .ToDictionaryAsync(x => x.UserName!, y => y.DisplayName, cancellation);
            return result;
        }

        public async Task<AppUserDto> GetAppUserDto(string userId, CancellationToken cancellation = default)
        {
            var key = $"GetAppUserDto:{userId}";
            //var result = await _cache.GetOrAddAsync(key,
            //    async () => await _userManager.Users.Where(x => x.Id == userId).Include(x => x.UserRoles)
            //        .ThenInclude(x => x.Role).ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
            //        .FirstOrDefaultAsync(cancellation) ?? new ApplicationUserDto(), Options); //Todo cache
            var result = await _userManager.Users.Where(x => x.Id == userId).Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .Select(x => x.AsDto())
                    .FirstOrDefaultAsync(cancellation) ?? new AppUserDto();
            return result;
        }



        public string GetUserName(string userId)
        {
            var key = $"GetUserName-byId:{userId}";
            //var user = _cache.GetOrAdd(key, () => _userManager.Users.SingleOrDefault(u => u.Id == userId), Options);
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user?.UserName ?? string.Empty;
        }

        public async Task<string?> GetUserNameAsync(string userId, CancellationToken cancellation = default)
        {
            var key = $"GetUserNameAsync:{userId}";
            //var user = await _cache.GetOrAddAsync(key,
            //    async () => await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId), Options);
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return user?.UserName;
        }


        public Task<bool> IsInRoleAsync(string userId, string role, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
