using Microsoft.EntityFrameworkCore;
using Restaurant.Model.Entity;
using Restaurant.Model.IRepository;
using Restaurants.infrastructure.Data;

namespace Restaurants.infrastructure.Repository
{
    internal class DishesRepository : IDishesRepository
    {
        private readonly AppDbContext context;

        public DishesRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Create(Dish dish)
        {
            await context.Dishes.AddAsync(dish);
            await context.SaveChangesAsync();
            return dish.Id;
        }

        public async Task Delete(IEnumerable<Dish> dishies)
        {
            context.Dishes.RemoveRange(dishies);
            await context.SaveChangesAsync();
        }

        public Task<bool> Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
