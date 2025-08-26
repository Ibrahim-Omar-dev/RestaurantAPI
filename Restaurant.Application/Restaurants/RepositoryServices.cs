using AutoMapper;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Model.Entity;
using Restaurant.Model.IRepository;

namespace Restaurant.Application.Restaurants
{
    public class RepositoryServices : IRepositoryServices
    {
        private readonly IRestaurantRepository restaurant;
        private readonly IMapper mapper;

        public RepositoryServices(IRestaurantRepository restaurant, IMapper mapper)
        {
            this.restaurant = restaurant;
            this.mapper = mapper;
        }

        public Task<int> CreateAsync(CreateRestaurantDto createRestaurantDto)
        {
            var newrestaurant = mapper.Map<Restaurantt>(createRestaurantDto);
            return restaurant.Create(newrestaurant);

        }

        public async Task<IEnumerable<Restaurantt>> GetAllAsync()
        {
            return await restaurant.GetAll();
        }

        public async Task<Restaurantt> GetByIdAsync(int id)
        {
            return await restaurant.GetById(id);
        }

        public async Task<Restaurantt> UpdateAsync(int id, UpdateRestaurantDto updateRestaurantDto)
        {
            var updateRestaurant = mapper.Map<Restaurantt>(updateRestaurantDto);
            restaurant.Update(id, updateRestaurant);
            return updateRestaurant;
        }
    }
}
