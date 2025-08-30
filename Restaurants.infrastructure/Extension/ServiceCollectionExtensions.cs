using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Model.IRepository;
using Restaurants.infrastructure.Data;
using Restaurants.infrastructure.Repository;
using Restaurants.infrastructure.Seeder;
using Restaurants.Infrastructure.Seeder;

namespace Restaurants.Infrastructure.Extension;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IRestaurantsSeeder, RestaurantsSeeder>();
        services.AddScoped(typeof(IRestaurantRepository), typeof(RestaurantRepository));
        services.AddScoped(typeof(IDishesRepository), typeof(DishesRepository));
    }
}
