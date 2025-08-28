using MediatR;

namespace Restaurant.Application.Command.DeleteRestaurant;

public class DeleteRestaurantCommand : IRequest<bool>
{
    public int Id { get; set; }
    public DeleteRestaurantCommand(int id)
    {
        Id = id;
    }
}
