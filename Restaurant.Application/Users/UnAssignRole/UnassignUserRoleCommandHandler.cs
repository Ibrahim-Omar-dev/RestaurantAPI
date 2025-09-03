using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Model.Entity;
using Restaurents_API.Exceptions;

namespace Restaurant.Application.Users.UnAssignRole
{
    public class UnassignUserRoleCommandHandler : IRequestHandler<UnassignUserRoleCommand>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> role;

        public UnassignUserRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> role)
        {
            this.userManager = userManager;
            this.role = role;
        }
        public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.UserEmail);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserEmail);
            }
            var Role = await role.FindByNameAsync(request.RoleName);
            if (Role == null)
            {
                throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            }
            await userManager.RemoveFromRoleAsync(user, Role.Name!);
        }
    }
}
