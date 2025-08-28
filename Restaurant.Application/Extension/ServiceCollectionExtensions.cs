using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Restaurants;

namespace Restaurant.Application.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
