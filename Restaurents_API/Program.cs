using Restaurant.Application.Extension;
using Restaurant.Model.Entity;
using Restaurants.infrastructure.Seeder;
using Restaurants.Infrastructure.Extension;
using Restaurents_API.Extension;
using Serilog;

namespace Restaurents_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();
            try
            {
                Log.Information("Starting up...");
                var builder = WebApplication.CreateBuilder(args);

                builder.AddPresentation();

                builder.Services.AddInfrastructure(builder.Configuration);
                builder.Services.AddApplication();

                var app = builder.Build();

                using (var scope = app.Services.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantsSeeder>();
                    await seeder.Seed();
                }

                // Configure pipeline
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurants API V1");
                        c.RoutePrefix = string.Empty;
                    });
                }

                app.UseHttpsRedirection();

                app.MapGroup("api/identity")
                    .WithTags("Identity")
                    .MapIdentityApi<User>();

                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();

                await app.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}