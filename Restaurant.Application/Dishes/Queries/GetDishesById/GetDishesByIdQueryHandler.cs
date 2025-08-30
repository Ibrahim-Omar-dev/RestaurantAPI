using AutoMapper;
using MediatR;
using Restaurant.Application.Dishes.Dtos;
using Restaurant.Model.IRepository;
using Restaurents_API.Exceptions;

namespace Restaurant.Application.Dishes.Queries.GetDishesById
{
    public class GetDishesByIdQueryHandler : IRequestHandler<GetDishesByIdQuery, DishDto>
    {
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IMapper mapper;

        public GetDishesByIdQueryHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            this.restaurantRepository = restaurantRepository;
            this.mapper = mapper;
        }
        public async Task<DishDto> Handle(GetDishesByIdQuery request, CancellationToken cancellationToken)
        {
            var restaurantId = request.RestaurantId;
            var dishId = request.DishId;
            // Logic to get the dish by restaurantId and dishId
            var restaurant = await restaurantRepository.GetById(restaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }
            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }
            var dishDto = mapper.Map<DishDto>(dish);
            return dishDto;
        }
    }
}
