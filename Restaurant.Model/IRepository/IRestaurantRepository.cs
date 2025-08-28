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
    }
}
