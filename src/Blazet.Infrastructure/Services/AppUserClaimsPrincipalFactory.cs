using Blazet.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Blazet.Infrastructure.Services
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        public AppUserClaimsPrincipalFactory(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            if (!string.IsNullOrEmpty(user.DisplayName))
                ((ClaimsIdentity)principal.Identity)?.AddClaims(new[]
                {
                new Claim(ClaimTypes.GivenName, user.DisplayName)
            });
            return principal;
        }
    }
}
