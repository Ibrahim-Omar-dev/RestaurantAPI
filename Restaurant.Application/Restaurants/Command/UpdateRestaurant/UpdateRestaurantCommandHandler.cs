using AutoMapper;
using MediatR;
using Restaurant.Model.IRepository;
using Restaurents_API.Exceptions;

namespace Restaurant.Application.Restaurants.Command.UpdateRestaurant
{
    class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
    {
        private readonly IRestaurantRepository restaurant;
        private readonly IMapper mapper;

        public UpdateRestaurantCommandHandler(IRestaurantRepository restaurant, IMapper mapper)
        {
            this.restaurant = restaurant;
            this.mapper = mapper;
        }
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurantEntity = await restaurant.GetById(request.Id);
            if (restaurantEntity == null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            mapper.Map(request, restaurantEntity);
            await restaurant.SaveChangeAsync();
        }

    }
}
