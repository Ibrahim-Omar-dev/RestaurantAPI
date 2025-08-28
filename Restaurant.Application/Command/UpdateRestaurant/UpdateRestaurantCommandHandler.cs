using AutoMapper;
using MediatR;
using Restaurant.Model.IRepository;

namespace Restaurant.Application.Command.UpdateRestaurant
{
    class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        private readonly IRestaurantRepository restaurant;
        private readonly IMapper mapper;

        public UpdateRestaurantCommandHandler(IRestaurantRepository restaurant, IMapper mapper)
        {
            this.restaurant = restaurant;
            this.mapper = mapper;
        }
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurantEntity = await restaurant.GetById(request.Id);
            if (restaurantEntity == null)
            {
                return false;
            }

            mapper.Map(request, restaurantEntity);
            await restaurant.SaveChangeAsync();
            return true;
        }

    }
}
