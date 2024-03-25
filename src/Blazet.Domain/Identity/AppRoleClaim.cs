using Microsoft.AspNetCore.Identity;

namespace Blazet.Domain.Identity
{
    public class AppRoleClaim : IdentityRoleClaim<string>
    {
        public string? Description { get; set; }
        public string? Group { get; set; }
        public virtual AppRole Role { get; set; } = default!;
    }
}
