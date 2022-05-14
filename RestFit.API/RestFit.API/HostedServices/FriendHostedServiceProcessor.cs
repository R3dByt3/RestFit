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

        public async Task<UnitGroup?> FetchNextAsync()
        {
            var search = new UnitSearch
            {
                NotProcessedBy = nameof(FriendHostedServiceProcessor)
            };
            return await _processorHub.SearchProcessor.GetUnitGroupAsync(search).ConfigureAwait(false);
        }

        public async Task ProcessAsync(Unit unit)
        {
            var search = new FriendSearch
            {
                FriendId = unit.UserId
            };
            
            var friend = await _processorHub.SearchProcessor.GetFriendAsync(search).ConfigureAwait(false);

            if (friend == null)
            {
                friend = new Friend
                {
                    
                };
            }
            else
            {

            }
        }
    }
}
