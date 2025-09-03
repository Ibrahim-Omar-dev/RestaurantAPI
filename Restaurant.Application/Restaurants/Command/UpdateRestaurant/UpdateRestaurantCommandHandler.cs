using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Model.Constant;
using Restaurant.Model.Entity;
using Restaurant.Model.Exceptions;
using Restaurant.Model.Interface;
using Restaurant.Model.IRepository;
using Restaurents_API.Exceptions;

namespace Restaurant.Application.Restaurants.Command.UpdateRestaurant
{
    class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
    {
        private readonly IRestaurantRepository restaurant;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateRestaurantCommand> logger;
        private readonly IRestaurantAuthorizationService restaurantAuthorization;

        public UpdateRestaurantCommandHandler(IRestaurantRepository restaurant, IMapper mapper
            , ILogger<UpdateRestaurantCommand> logger, IRestaurantAuthorizationService restaurantAuthorization)
        {
            this.restaurant = restaurant;
            this.mapper = mapper;
            this.logger = logger;
            this.restaurantAuthorization = restaurantAuthorization;
        }
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateRestaurantCommandHandler called with id: {Id}", request.Id);

            var restaurantEntity = await restaurant.GetById(request.Id);
            if (restaurantEntity == null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            if (!restaurantAuthorization.Authorize(restaurantEntity, ResourceOperation.Update))
                throw new ForbidException();
            mapper.Map(request, restaurantEntity);

            await restaurant.SaveChangeAsync();
        }

    }
}
