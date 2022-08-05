using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MongoDbService
{
    public static class ServiceRegistration
    {
        public static IServiceCollection UseMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection("MongoOptions"));
            services.AddSingleton<IMongoService, MongoService>();
            return services;
        }
    }
}
