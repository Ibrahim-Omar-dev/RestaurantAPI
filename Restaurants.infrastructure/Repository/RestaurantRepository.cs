using Microsoft.EntityFrameworkCore;
using Restaurant.Model.Constant;
using Restaurant.Model.Entity;
using Restaurant.Model.IRepository;
using Restaurants.infrastructure.Data;
using System.Linq.Expressions;

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
            return await context.Restaurants.Include(x => x.Dishes).ToListAsync();
        }

        public async Task<(IEnumerable<Restaurantt> Restaurants, int TotalCount)>
     GetAllMatching(string? searchPhrase, int pageSize = 5, int pageNumber = 1, string? sortedBy = null, SortDirection? sortedDirection = null)
        {
            IQueryable<Restaurantt> query = context.Restaurants.Include(r => r.Dishes);

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                string search = searchPhrase.ToLower();
                query = query.Where(r =>
                    (r.Name ?? "").ToLower().Contains(search) ||
                    (r.Description ?? "").ToLower().Contains(search) ||
                    (r.Category ?? "").ToLower().Contains(search));
            }

            int count = await query.CountAsync();

            // Apply sorting
            if (sortedBy != null)
            {
                var dict = new Dictionary<string, Expression<Func<Restaurantt, object>>>
                {
                    {nameof(Restaurantt.Name),r=>r.Name!},
                    {nameof(Restaurantt.Description),r=>r.Description!},
                    {nameof(Restaurantt.Category),r=>r.Category!},
                };
                if (dict.ContainsKey(sortedBy))
                {
                    var selectedColumn = dict[sortedBy];
                    query = sortedDirection == SortDirection.Descending ?
                        query.OrderByDescending(selectedColumn) :
                        query.OrderBy(selectedColumn);
                }
            }

            if (pageSize > 0 && pageNumber > 0)
            {
                query = query
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize);
            }

            var restaurants = await query.ToListAsync();

            return (restaurants, count);
        }



        public async Task<Restaurantt> GetById(int id)
        {
            var restaurant = await context.Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync(r => r.Id == id);
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
