using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Restaurants;
using Restaurant.Application.Users;

namespace Restaurant.Application.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
            services.AddScoped<IUserContext, UserContext>();

        }
    }
}
