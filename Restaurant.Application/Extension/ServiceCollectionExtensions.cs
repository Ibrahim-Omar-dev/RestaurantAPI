using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace Restaurant.Application.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
            services.AddScoped<IUserContext, UserContext>();

            services.AddValidatorsFromAssembly(assembly)
               .AddFluentValidationAutoValidation();
        }
    }
}
