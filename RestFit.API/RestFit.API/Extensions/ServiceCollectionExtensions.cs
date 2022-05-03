using RestFit.API.Services;
using RestFit.DataAccess;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartup(this IServiceCollection services)
        {
            return services.AddSingleton<IUserAccess, UserAccess>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<IUnitRepository, UnitRepository>()
                .AddSingleton<IUnitAccess, UnitAccess>();
        }
    }
}
