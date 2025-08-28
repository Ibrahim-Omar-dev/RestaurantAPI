using Microsoft.EntityFrameworkCore;
using Restaurant.Model.Entity;
using Restaurant.Model.IRepository;
using Restaurants.infrastructure.Data;

namespace Restaurants.infrastructure.Repository
{
    internal class RestaurantRepository : IRestaurantRepository
    {
        private readonly AppDbContext context;

        public RestaurantRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Create(Restaurantt restaurant)
        {
            await context.Restaurants.AddAsync(restaurant);
            await context.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task Delete(Restaurantt restaurantt)
        {
            context.Restaurants.Remove(restaurantt);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurantt>> GetAll()
        {
            return await context.Restaurants.ToListAsync();
        }

        public async Task<Restaurantt> GetById(int id)
        {
            var restaurant = await context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
            return restaurant;
        }

        public async Task SaveChangeAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<Restaurantt> Update(int id, Restaurantt restaurant)
        {
            var oldRestaurant = await context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
            if (oldRestaurant == null)
            {
                return null;
            }
            context.Update(restaurant);
            await context.SaveChangesAsync();
            return restaurant;
        }
    }
}
