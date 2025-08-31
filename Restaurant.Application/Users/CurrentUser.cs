namespace Restaurant.Application.Users
{
    public class CurrentUser
    {
        public string Id { get; }
        public string Email { get; }
        public IEnumerable<string> Roles { get; }
        public string? Nationality { get; }
        public DateOnly? DateOfBirth { get; }

        public CurrentUser(string id, string email, IEnumerable<string> roles, string? nationality = null, DateOnly? dateOfBirth = null)
        {
            Id = id;
            Email = email;
            Roles = roles;
            Nationality = nationality;
            DateOfBirth = dateOfBirth;
        }

        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
