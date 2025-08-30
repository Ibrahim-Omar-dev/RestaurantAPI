using Restaurant.Model.Entity;

namespace Restaurant.Model.IRepository
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish dish);
        Task<bool> Update(int id);
        Task Delete(IEnumerable<Dish> dish);
    }
}
