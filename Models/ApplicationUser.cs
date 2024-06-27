using Microsoft.AspNetCore.Identity;

namespace PawsAndTailsWebAPISwagger.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsAdmin { get; set; }
    }
}
