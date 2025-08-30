using MediatR;

namespace Restaurant.Application.Dishes.Command.DeletDishCommand
{
    public class DeleteDishesForRestaurantCommand : IRequest
    {
        public int RestaurantId { get; set; }

        public DeleteDishesForRestaurantCommand(int restaurantId)
        {
            RestaurantId = restaurantId;
        }
    }
}
