using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace TechCards.Repository.ServiceExtension
{
    public static class TechRepoServiceExtension
    {
        public static IServiceCollection AddMongoDbClient(this IServiceCollection services,
            string connectionString)
        {
            var mongourl = MongoUrl.Create(connectionString);

            var mongoClient = new MongoClient(mongourl);
            services.AddSingleton<MongoUrl>(mongourl);
            services.AddSingleton<IMongoClient>(mongoClient);
            return services;

        }
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, 
            string mongoConnectionstring)
        {
            services.AddMongoDbClient(mongoConnectionstring);
            services.AddSingleton<IFCDetailRepo, FCDetailRepo>();
            services.AddSingleton<IFCRepo, FCRepo>();
            return services;
        }
    }
}
