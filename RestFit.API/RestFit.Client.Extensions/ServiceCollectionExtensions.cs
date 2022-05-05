using Microsoft.Extensions.DependencyInjection;

namespace RestFit.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClient(this IServiceCollection services)
        {
            return services;
        }
    }
}
