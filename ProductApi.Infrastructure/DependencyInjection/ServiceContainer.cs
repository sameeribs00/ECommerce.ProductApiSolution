using ECommerece.CommonLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.IServices;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Services;

namespace ProductApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService (this IServiceCollection serviceCollection, IConfiguration config)
        {
            //Add database connectivity
            //Add authentication scheme
            SharedServiceContainer.AddSharedService<ProductDbContext>(serviceCollection, config, config["MySerilog:FileName"]!);

            //Inject product service
            serviceCollection.AddScoped<IProductService, ProductService>();
            return serviceCollection;
        }
        public static IApplicationBuilder UseInfrastructureAppBuilder (this IApplicationBuilder app)
        {
            SharedServiceContainer.AddSharedMiddlewares(app);
            return app;
        }
    }
}
