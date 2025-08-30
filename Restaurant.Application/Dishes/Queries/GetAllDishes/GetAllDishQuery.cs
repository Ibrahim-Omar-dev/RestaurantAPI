using MediatR;
using Restaurant.Application.Dishes.Dtos;
using Restaurant.Model.Entity;

namespace Restaurant.Application.Dishes.Queries.GetAllDishes
{
    public class GetAllDishQuery : IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; set; }

        public GetAllDishQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }
    }
}
