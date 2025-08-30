using AutoMapper;
using MediatR;
using Restaurant.Application.Dishes.Dtos;
using Restaurant.Model.Entity;
using Restaurant.Model.IRepository;
using Restaurents_API.Exceptions;

namespace Restaurant.Application.Dishes.Queries.GetAllDishes
{
    public class GetAllDishQueryHandler : IRequestHandler<GetAllDishQuery, IEnumerable<DishDto>>
    {
        private readonly IRestaurantRepository restaurantt;
        private readonly IMapper mapper;

        public GetAllDishQueryHandler(IRestaurantRepository restaurantt, IMapper mapper)
        {
            this.restaurantt = restaurantt;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<DishDto>> Handle(GetAllDishQuery request, CancellationToken cancellationToken)
        {
            var restaurantId = request.RestaurantId;
            var restaurants = await restaurantt.GetById(restaurantId);
            if (restaurants == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var result = mapper.Map<IEnumerable<DishDto>>(restaurants.Dishes);

            return result;

        }
    }
}
