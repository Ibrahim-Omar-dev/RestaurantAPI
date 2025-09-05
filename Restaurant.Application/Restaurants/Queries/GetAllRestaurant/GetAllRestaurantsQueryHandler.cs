using AutoMapper;
using MediatR;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Model.IRepository;
using Restaurants.Application.Common;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurant;
public class GetAllRestaurantsQueryHandler(
    IMapper mapper,
    IRestaurantRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantQuery, PageResult<RestaurantDto>>
{
    public async Task<PageResult<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
    {
        var searchPhraseLower = request.SearchPhrase?.ToLower();
        var (restaurants, count) = await restaurantsRepository.GetAllMatching(searchPhraseLower
            , request.PageSize, request.PageNumber, request.SortedBy, request.SortDirection);
        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        var result = new PageResult<RestaurantDto>(restaurantsDtos, count, request.PageSize, request.PageNumber);

        return result;
    }
}
