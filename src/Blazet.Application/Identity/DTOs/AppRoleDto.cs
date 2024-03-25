using System.ComponentModel;

namespace Blazet.Application.Identity.DTOs
{
    [Description("Roles")]
    public class AppRoleDto
    {
        [Description("Id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Description("Name")]
        public string Name { get; set; } = string.Empty;

        [Description("Normalized Name")]
        public string? NormalizedName { get; set; }

        [Description("Description")]
        public string? Description { get; set; }
    }
}
