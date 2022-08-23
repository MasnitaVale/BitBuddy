using Microsoft.AspNetCore.Identity;

namespace BitBuddy.Core.Entities
{
    public static class AppUserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
    public class UserRole : IdentityRole
    {

    }
}
