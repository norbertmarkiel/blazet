using Microsoft.AspNetCore.Identity;

namespace Blazet.Domain.Identity
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        {
            RoleClaims = new HashSet<AppRoleClaim>();
            UserRoles = new HashSet<AppUserRole>();
        }
        public AppRole(string roleName) : base(roleName)
        {
            RoleClaims = new HashSet<AppRoleClaim>();
            UserRoles = new HashSet<AppUserRole>();
        }
        public string? Description { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
