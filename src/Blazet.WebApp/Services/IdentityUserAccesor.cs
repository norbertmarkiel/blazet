using Blazet.Domain.Identity;
using Blazet.WebApp.Core.Constants;
using Microsoft.AspNetCore.Identity;

namespace Blazet.WebApp.Services
{
    internal sealed class IdentityUserAccesor(
         UserManager<AppUser> userManager,
        IdentityRedirectManager redirectManager)
    {
        public async Task<AppUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
                redirectManager.RedirectToWithStatus(PageRoutes.InvalidUser,
                    $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);

            return user;
        }
    }
}
