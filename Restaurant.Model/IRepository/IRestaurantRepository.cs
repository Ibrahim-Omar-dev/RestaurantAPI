using Restaurant.Model.Constant;
using Restaurant.Model.Entity;

namespace Restaurant.Model.IRepository
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurantt>> GetAll();
        Task<Restaurantt> GetById(int id);
        Task<int> Create(Restaurantt restaurant);
        Task Delete(Restaurantt restaurantt);
        Task SaveChangeAsync();
        Task<(IEnumerable<Restaurantt> Restaurants, int TotalCount)> GetAllMatching(string? searchPhrase, int pageSize = 5, int pageNumber = 1, string? sortedBy = null, SortDirection? sortedDirection = null);
    }
}
