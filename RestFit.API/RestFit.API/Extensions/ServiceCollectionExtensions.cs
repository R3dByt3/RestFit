using RestFit.API.Services;
using RestFit.Repository;
using RestFit.Repository.Abstract;

namespace RestFit.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartup(this IServiceCollection services)
        {
            return services.AddSingleton<IUserAccess, UserAccess>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<IUserService, UserService>();
        }
    }
}
