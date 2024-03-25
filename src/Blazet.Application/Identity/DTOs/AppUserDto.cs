using System.ComponentModel;

namespace Blazet.Application.Identity.DTOs
{
    [Description("Users")]

    public class AppUserDto
    {
        [Description("User Id")] public string Id { get; set; } = string.Empty;
        [Description("User Name")] public string UserName { get; set; } = string.Empty;
        [Description("Display Name")] public string? DisplayName { get; set; }
        [Description("Provider")] public string? Provider { get; set; } = "BlazetDevelopment";
        [Description("Email")] public string Email { get; set; } = string.Empty;
        [Description("Phone Number")] public string? PhoneNumber { get; set; }
        [Description("Assigned Roles")] public string[]? AssignedRoles { get; set; }
        [Description("Default Role")] public string? DefaultRole => AssignedRoles?.FirstOrDefault();
        [Description("Is Active")] public bool IsActive { get; set; }
        [Description("Password")] public string? Password { get; set; }
        [Description("Confirm Password")] public string? ConfirmPassword { get; set; }
        [Description("Status")] public DateTimeOffset? LockoutEnd { get; set; }

        public bool IsInRole(string role)
        {
            return AssignedRoles?.Contains(role) ?? false;
        }
    }
}
