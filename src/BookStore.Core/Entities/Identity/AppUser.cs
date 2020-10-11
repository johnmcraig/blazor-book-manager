using Microsoft.AspNetCore.Identity;

namespace BookStore.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
