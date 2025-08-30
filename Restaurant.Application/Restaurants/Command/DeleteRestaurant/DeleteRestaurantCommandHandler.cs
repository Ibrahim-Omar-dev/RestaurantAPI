using MediatR;
using Restaurant.Model.IRepository;
using Restaurents_API.Exceptions;

namespace Restaurant.Application.Restaurants.Command.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly IRestaurantRepository restaurant;

        public DeleteRestaurantCommandHandler(IRestaurantRepository restaurant)
        {
            this.restaurant = restaurant;
        }
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurantt = await restaurant.GetById(request.Id);

            if (restaurantt == null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            await restaurant.Delete(restaurantt);
        }
    }
}
