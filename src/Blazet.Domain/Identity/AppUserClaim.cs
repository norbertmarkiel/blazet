using Microsoft.AspNetCore.Identity;

namespace Blazet.Domain.Identity
{
    public class AppUserClaim : IdentityUserClaim<string>
    {
        public string? Description { get; set; }
        public virtual AppUser User { get; set; } = default!;
    }
}
