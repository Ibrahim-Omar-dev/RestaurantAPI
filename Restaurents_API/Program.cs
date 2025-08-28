using Restaurants.infrastructure.Seeder;
using Restaurants.Infrastructure.Extension;
using Restaurant.Application.Extension;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Restaurents_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register Swagger (Swashbuckle)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Infrastructure & Application layers
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();

            Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // ?? important, shows SQL
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger()

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurants API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            // Seed database
            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantsSeeder>();
                await seeder.Seed();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
