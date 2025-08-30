using Restaurant.Application.Extension;
using Restaurants.infrastructure.Seeder;
using Restaurants.Infrastructure.Extension;
using Serilog;

namespace Restaurents_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // configure Serilog first
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            try
            {
                Log.Information("Starting up...");

                var builder = WebApplication.CreateBuilder(args);

                // full Serilog config from appsettings.json
                builder.Host.UseSerilog((context, services, configuration) =>
                    configuration.ReadFrom.Configuration(context.Configuration)
                                 .ReadFrom.Services(services)
                                 .Enrich.FromLogContext()
                                 .WriteTo.Console()
                );

                // services
                builder.Services.AddScoped<ErrorHandleing>();
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddInfrastructure(builder.Configuration);
                builder.Services.AddApplication();

                var app = builder.Build();

                // global error handler middleware
                app.UseMiddleware<ErrorHandleing>();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurants API V1");
                        c.RoutePrefix = string.Empty; // Swagger at root
                    });
                }

                // run DB seeder safely
                using (var scope = app.Services.CreateScope())
                {
                    try
                    {
                        var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantsSeeder>();
                        await seeder.Seed();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Error while seeding the database");
                    }
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();

                // serve static files (wwwroot/index.html if present)
                app.UseStaticFiles();

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
