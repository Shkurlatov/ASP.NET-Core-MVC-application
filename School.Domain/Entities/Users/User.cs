using Microsoft.AspNetCore.Identity;

namespace School.Domain.Entities
{
    public abstract class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
