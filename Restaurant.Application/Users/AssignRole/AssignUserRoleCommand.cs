using MediatR;

namespace Restaurant.Application.Users.AssignRole;

public class AssignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; }
    public string RoleName { get; set; }
}
