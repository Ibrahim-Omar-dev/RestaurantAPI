using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Users;
using Restaurant.Model.Constant;
using Restaurant.Model.Entity;
using Restaurant.Model.Interface;
using Restaurant.Model.IRepository;

namespace Restaurant.Application.Restaurants.Command.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private readonly ILogger<CreateRestaurantCommand> logger;
        private readonly IMapper mapper;
        private readonly IRestaurantRepository restaurant;
        private readonly IUserContext context;
        private readonly IRestaurantAuthorizationService restaurantAuthorizationService;

        public CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommand> logger, IMapper mapper, IRestaurantRepository restaurant,
            IUserContext context)
        {
            logger = logger;
            this.mapper = mapper;
            this.restaurant = restaurant;
            this.context = context;
            this.restaurantAuthorizationService = restaurantAuthorizationService;
        }
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new restaurant {RestaurantName}", request.Name);
            var currentUser = context.GetCurrentUser();
            var newrestaurant = mapper.Map<Restaurantt>(request);
            newrestaurant.OwnerId = currentUser.Id;
            return await restaurant.Create(newrestaurant);
        }
    }
}
