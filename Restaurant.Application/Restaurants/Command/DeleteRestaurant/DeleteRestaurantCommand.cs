using MediatR;

namespace Restaurant.Application.Restaurants.Command.DeleteRestaurant;

public class DeleteRestaurantCommand : IRequest
{
    public int Id { get; set; }
    public DeleteRestaurantCommand(int id)
    {
        Id = id;
    }
}
