using System.ComponentModel.DataAnnotations;

namespace Blazet.WebApp.Components.Pages.Identity.Models
{
    public sealed class LoginModel
    {
        [Required(ErrorMessage = "User name cannot be empty")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Password cannot be empty")]
        [StringLength(30, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
        public string Password { get; set; } = "";
        public bool RememberMe { get; set; } = false;
    }
}
