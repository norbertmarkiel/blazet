using Blazet.Application.Common.Interfaces.Identity;

namespace Blazet.Infrastructure.Configuration
{
    public class IdentitySettings : IIdentitySettings
    {
        public const string Key = nameof(IdentitySettings);

        public bool RequireDigit { get; set; } = true;
        public int RequiredLength { get; set; } = 4;
        public int MaxLength { get; set; } = 30;
        public bool RequireNonAlphanumeric { get; set; } = true;
        public bool RequireUpperCase { get; set; } = true;
        public bool RequireLowerCase { get; set; } = false;
        public int DefaultLockoutTimeSpan { get; set; } = 15;
    }
}
