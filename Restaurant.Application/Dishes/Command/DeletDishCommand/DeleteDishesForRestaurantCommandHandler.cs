using AutoMapper;
using MediatR;
using Restaurant.Model.IRepository;
using Restaurents_API.Exceptions;

namespace Restaurant.Application.Dishes.Command.DeletDishCommand
{
    public class DeleteDishesForRestaurantCommandHandler : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IDishesRepository dishesRepository;
        private readonly IMapper mapper;

        public DeleteDishesForRestaurantCommandHandler(IRestaurantRepository restaurantRepository
            , IDishesRepository dishesRepository, IMapper mapper)
        {
            this.restaurantRepository = restaurantRepository;
            this.dishesRepository = dishesRepository;
            this.mapper = mapper;
        }
        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepository.GetById(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());


            await dishesRepository.Delete(restaurant.Dishes);

        }
    }
}
