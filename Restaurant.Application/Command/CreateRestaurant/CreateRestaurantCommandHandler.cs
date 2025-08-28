using AutoMapper;
using MediatR;
using Restaurant.Application.Restaurants;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Model.Entity;
using Restaurant.Model.IRepository;

namespace Restaurant.Application.Command.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private readonly IMapper mapper;
        private readonly IRestaurantRepository restaurant;

        public CreateRestaurantCommandHandler(IMapper mapper, IRestaurantRepository restaurant)
        {
            this.mapper = mapper;
            this.restaurant = restaurant;
        }
        public Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var newrestaurant = mapper.Map<Restaurantt>(request);
            return restaurant.Create(newrestaurant);
        }
    }
}
