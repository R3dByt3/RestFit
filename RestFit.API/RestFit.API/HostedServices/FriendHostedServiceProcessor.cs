using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.Logic.Abstract;

namespace RestFit.API.HostedServices
{
    public class FriendHostedServiceProcessor : IFriendHostedServiceProcessor
    {
        private readonly IProcessorHub _processorHub;

        public FriendHostedServiceProcessor(IProcessorHub processorHub)
        {
            _processorHub = processorHub;
        }

        public async Task<UserGroupedUnit?> FetchNextAsync()
        {
            var search = new UnitSearch
            {
                NotProcessedBy = nameof(FriendHostedServiceProcessor)
            };
            return await _processorHub.SearchProcessor.GetUnitGroupAsync(search).ConfigureAwait(false);
        }

        public async Task ProcessAsync(UserGroupedUnit unitGroup)
        {
            var search = new UnitGroupSearch();
        }
    }
}
