using Microsoft.Extensions.DependencyInjection;
using Repository.DBContext;

namespace Repository.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositoryExtensions(this IServiceCollection services)
        {
            // Add your repository extensions here
            // Example: services.AddScoped<IYourRepository, YourRepository>();
            services.AddScoped<BulkyDBContext>();

            return services;
        }
    }
}
