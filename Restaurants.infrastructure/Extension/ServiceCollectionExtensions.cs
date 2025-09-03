using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Model.Entity;
using Restaurant.Model.Interface;
using Restaurant.Model.IRepository;
using Restaurants.infrastructure.Authorization;
using Restaurants.infrastructure.Authorization.Constant;
using Restaurants.infrastructure.Authorization.Services;
using Restaurants.infrastructure.Data;
using Restaurants.infrastructure.Repository;
using Restaurants.infrastructure.Seeder;

namespace Restaurants.Infrastructure.Extension;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddIdentityCore<User>()
           .AddRoles<IdentityRole>()
           .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
           .AddEntityFrameworkStores<AppDbContext>();


        // Add Identity API endpoints separately
        services.AddIdentityApiEndpoints<User>();

        services.AddScoped<IRestaurantsSeeder, RestaurantsSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();

        services.AddAuthorizationBuilder()
                .AddPolicy(PolicyName.HasNationality, policy =>
                   policy.RequireClaim(AppClaimTypes.Nationality, "Franch", "American"));

        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
    }
}