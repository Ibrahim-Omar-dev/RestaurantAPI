using MediatR;

namespace Restaurant.Application.Users.Command
{
    public class UpdateUserDetailsCommand : IRequest
    {
        public DateOnly? BirthDate { get; set; }
        public string? Nationality { get; set; }
    }
}
