using MediatR;
using Restaurant.Application.Dishes.Dtos;

namespace Restaurant.Application.Dishes.Queries.GetDishesById
{
    public class GetDishesByIdQuery : IRequest<DishDto>
    {
        public GetDishesByIdQuery(int restaurantId, int dishId)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
        }

        public int RestaurantId { get; set; }
        public int DishId { get; set; }
    }
}
