using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Model.Constant;
using Restaurant.Model.Exceptions;
using Restaurant.Model.Interface;
using Restaurant.Model.IRepository;
using Restaurents_API.Exceptions;

namespace Restaurant.Application.Restaurants.Command.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly IRestaurantRepository restaurant;
        private readonly ILogger<DeleteRestaurantCommand> logger;
        private readonly IRestaurantAuthorizationService restaurantAuthorizationService;

        public DeleteRestaurantCommandHandler(IRestaurantRepository restaurant
            , ILogger<DeleteRestaurantCommand> logger, IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            this.restaurant = restaurant;
            this.logger = logger;
            this.restaurantAuthorizationService = restaurantAuthorizationService;
        }
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteRestaurantCommandHandler called with id: {Id}", request.Id);
            var restaurantt = await restaurant.GetById(request.Id);

            if (restaurantt == null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurantt, ResourceOperation.Delete))
                throw new ForbidException();

            await restaurant.Delete(restaurantt);
        }
    }
}
