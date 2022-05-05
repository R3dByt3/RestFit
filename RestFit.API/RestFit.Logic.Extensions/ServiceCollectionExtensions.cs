using Microsoft.Extensions.DependencyInjection;
using RestFit.Logic.Abstract;

namespace RestFit.Logic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLogic(this IServiceCollection services)
        {
            return services.AddSingleton<IInsertProcessor, InsertProcessor>()
                .AddSingleton<IDeleteProcessor, DeleteProcessor>()
                .AddSingleton<ISearchProcessor, SearchProcessor>()
                .AddSingleton<IUpdateProcessor, UpdateProcessor>()
                .AddSingleton<IProcessorHub, ProcessorHub>();
        }
    }
}
