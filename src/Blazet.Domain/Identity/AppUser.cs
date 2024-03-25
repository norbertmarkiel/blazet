using Microsoft.AspNetCore.Identity;

namespace Blazet.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            UserClaims = new HashSet<AppUserClaim>();
            UserRoles = new HashSet<AppUserRole>();
            Logins = new HashSet<AppUserLogin>();
            Tokens = new HashSet<AppUserToken>();
        }
        public string? DisplayName { get; set; }
        public string? Provider { get; set; } = "BlazetDevelopment";
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<AppUserClaim> UserClaims { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppUserLogin> Logins { get; set; }
        public virtual ICollection<AppUserToken> Tokens { get; set; }

    }
}
