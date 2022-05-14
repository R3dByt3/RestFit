using Microsoft.Extensions.DependencyInjection;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            return services.AddSingleton<IUserAccess, UserAccess>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<IUnitRepository, UnitRepository>()
                .AddSingleton<IUnitAccess, UnitAccess>()
                .AddSingleton<IRepositoryHub, RepositoryHub>()
                .AddSingleton<IFriendAccess, FriendAccess>()
                .AddSingleton<IFriendRepository, FriendRepository>();
        }
    }
}
