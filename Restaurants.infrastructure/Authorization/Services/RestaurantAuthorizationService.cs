using Microsoft.Extensions.Logging;
using Restaurant.Application.Users;
using Restaurant.Model.Constant;
using Restaurant.Model.Entity;
using Restaurant.Model.Interface;

namespace Restaurants.infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService : IRestaurantAuthorizationService
    {
        private readonly ILogger<Restaurantt> logger;
        private readonly IUserContext context;

        public RestaurantAuthorizationService(ILogger<Restaurantt> logger, IUserContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public bool Authorize(Restaurantt restaurant, ResourceOperation resourceOperation)
        {
            var user = context.GetCurrentUser();
            logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
                                user.Email,
                                resourceOperation,
                                restaurant.Name);

            if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }
            if (user.IsInRole(UserRoles.Admin) && resourceOperation == ResourceOperation.Delete)
            {
                logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }
            if (resourceOperation == ResourceOperation.Update || resourceOperation == ResourceOperation.Delete)
            {
                if (restaurant.OwnerId == user.Id)
                {
                    logger.LogInformation("Owner user, update/delete operation - successful authorization");
                    return true;
                }
                else
                {
                    logger.LogWarning("User {UserEmail} is not the owner of restaurant {RestaurantName} - authorization failed",
                                      user.Email,
                                      restaurant.Name);
                    return false;
                }
            }
            return false;
        }
    }
}
