using MediatR;
using Restaurant.Model.IRepository;

namespace Restaurant.Application.Command.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        private readonly IRestaurantRepository restaurant;

        public DeleteRestaurantCommandHandler(IRestaurantRepository restaurant)
        {
            this.restaurant = restaurant;
        }
        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurantt = await restaurant.GetById(request.Id);

            if (restaurantt == null)
            {
                return false;
            }

            await restaurant.Delete(restaurantt);
            return true;
        }

    }
}
