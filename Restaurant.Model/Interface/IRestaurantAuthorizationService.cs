using Restaurant.Model.Constant;
using Restaurant.Model.Entity;

namespace Restaurant.Model.Interface
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurantt restaurantId, ResourceOperation resourceOperation);
    }
}
