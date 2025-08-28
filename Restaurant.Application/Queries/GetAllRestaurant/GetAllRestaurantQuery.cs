using MediatR;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Queries.GetAllRestaurant;

public class GetAllRestaurantQuery : IRequest<IEnumerable<RestaurantDto>>
{
}
