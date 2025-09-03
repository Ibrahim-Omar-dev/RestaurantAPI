using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Model.Entity;
using Restaurents_API.Exceptions;

namespace Restaurant.Application.Users.AssignRole
{
    public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AssignUserRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            var Role = await roleManager.FindByNameAsync(request.RoleName);
            if (Role == null)
            {
                throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            }
            var user = await userManager.FindByEmailAsync(request.UserEmail);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserEmail);
            }
            await userManager.AddToRoleAsync(user, Role.Name!);
        }
    }
}
