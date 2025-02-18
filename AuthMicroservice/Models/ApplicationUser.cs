using Microsoft.AspNetCore.Identity;

namespace AuthMicroservice.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string RoleType { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = string.Empty;
        public int Ci { get; set; }
    }
}