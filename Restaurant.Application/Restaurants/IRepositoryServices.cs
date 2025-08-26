using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Model.Entity;

namespace Restaurant.Application.Restaurants
{
    public interface IRepositoryServices
    {
        Task<IEnumerable<Restaurantt>> GetAllAsync();
        Task<Restaurantt> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateRestaurantDto createRestaurantDto);
        Task<Restaurantt> UpdateAsync(int id, UpdateRestaurantDto updateRestaurant);
    }
}