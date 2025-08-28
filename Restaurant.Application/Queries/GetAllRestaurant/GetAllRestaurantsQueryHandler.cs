using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Model.IRepository;

namespace Restaurant.Application.Queries.GetAllRestaurant;
public class GetAllRestaurantsQueryHandler(
    IMapper mapper,
    IRestaurantRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
    {
        var restaurants = await restaurantsRepository.GetAll();
        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantsDtos;
    }
}
