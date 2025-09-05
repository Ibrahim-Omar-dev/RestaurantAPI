using MediatR;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Model.Constant;
using Restaurants.Application.Common;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurant;

public class GetAllRestaurantQuery : IRequest<PageResult<RestaurantDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? SortedBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
