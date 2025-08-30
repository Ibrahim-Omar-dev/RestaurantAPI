using AutoMapper;
using MediatR;
using Restaurant.Application.Dishes.Dtos;
using Restaurant.Model.Entity;
using Restaurant.Model.IRepository;
using Restaurents_API.Exceptions;

namespace Restaurant.Application.Dishes.Command.CreateDishCommand
{
    class CreateDishesCommandHandler : IRequestHandler<CreateDishesCommand, int>
    {
        private readonly IDishesRepository dishes;
        private readonly IRestaurantRepository restaurant;
        private readonly IMapper mapper;

        public CreateDishesCommandHandler(IDishesRepository dishes, IRestaurantRepository restaurant, IMapper mapper)
        {
            this.dishes = dishes;
            this.restaurant = restaurant;
            this.mapper = mapper;
        }
        public Task<int> Handle(CreateDishesCommand request, CancellationToken cancellationToken)
        {
            var restaurantt = restaurant.GetById(request.RestaurantId);
            if (restaurantt == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var dish = mapper.Map<Dish>(request);
            return dishes.Create(dish);
        }
    }
}
