using Microsoft.AspNetCore.Identity;

namespace Restaurant.Model.Entity
{
    public class User : IdentityUser
    {
        public DateOnly? BirthDate { get; set; }
        public string? Nationality { get; set; }
    }
}
