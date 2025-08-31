using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Model.Entity;
using Restaurents_API.Exceptions;

namespace Restaurant.Application.Users.Command
{
    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand>
    {
        private readonly IUserContext userContext;
        private readonly IUserStore<User> userStore;

        public UpdateUserDetailsCommandHandler(IUserContext userContext, IUserStore<User> userStore)
        {
            this.userContext = userContext;
            this.userStore = userStore;
        }
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            if (currentUser == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            // Find user in the store (DB)
            var dbUser = await userStore.FindByIdAsync(currentUser.Id, cancellationToken);
            if (dbUser == null)
            {
                throw new NotFoundException(nameof(User), currentUser.Id);
            }

            // Update properties
            dbUser.Nationality = request.Nationality;
            dbUser.BirthDate = request.BirthDate;

            // Save changes
            var result = await userStore.UpdateAsync(dbUser, cancellationToken);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to update user {currentUser.Id}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}
