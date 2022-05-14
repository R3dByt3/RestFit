using RestFit.API.Controllers.v1;
using RestFit.API.HostedServices;
using RestFit.API.Services;
using RestFit.Client.Extensions;
using RestFit.DataAccess.Extensions;
using RestFit.Logic.Extensions;

namespace RestFit.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartup(this IServiceCollection services)
        {
            return services.AddSingleton<IUserService, UserService>()
                .AddSingleton<IUserControllerV1Processor, UserControllerV1Processor>()
                .AddSingleton<IUnitControllerV1Processor, UnitControllerV1Processor>()
                .AddSingleton<IFriendHostedServiceProcessor, FriendHostedServiceProcessor>()
                .AddHostedService<FriendHostedService>()
                .AddClient()
                .AddDataAccess()
                .AddLogic()
                .AddHttpContextAccessor();
        }
    }
}
