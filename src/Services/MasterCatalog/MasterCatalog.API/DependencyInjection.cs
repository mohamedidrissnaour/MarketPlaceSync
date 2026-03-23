using Microsoft.OpenApi;

namespace MasterCatalog.API;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1" , new OpenApiInfo
            {
                Title = "MasterCatalog API",
                Version = "v1",
                Description = "API de gestion du catalogue produits canoniques - MarketplaceSync"
            });
        });
        return services;
    }
    }
