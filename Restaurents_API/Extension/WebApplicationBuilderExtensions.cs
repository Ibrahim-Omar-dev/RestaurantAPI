using Microsoft.OpenApi.Models;
using Serilog;

namespace Restaurents_API.Extension
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, configuration) =>
                    configuration.ReadFrom.Configuration(context.Configuration)
                                 .ReadFrom.Services(services)
                                 .Enrich.FromLogContext()
                                 .WriteTo.Console()
                );
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Enter JWT Bearer token"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearerAuth"
                                }
                            },
                            new string[] {}
                        }
                    });
            });
            builder.Services.AddScoped<ErrorHandleing>();
        }
    }
}
