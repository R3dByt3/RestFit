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
            return await _processorHub.SearchProcessor.AggregateUserGroupedUnitAsync(search).ConfigureAwait(false);
        }

        public async Task ProcessAsync(UserGroupedUnit unitGroup)
        {
            var search = new UserGroupedUnitSearch
            {
                UserId = unitGroup.UserId,
                Type = unitGroup.Type
            };
            
            var unit = await _processorHub.SearchProcessor.GetUserGroupedUnitAsync(search).ConfigureAwait(false);

            if (unit == null)
            {
                await _processorHub.InsertProcessor.CreateUserGroupedUnitAsync(unitGroup).ConfigureAwait(false);
            }
            else
            {
                await _processorHub.UpdateProcessor.UpdateUserGroupedUnitAggregations(unitGroup).ConfigureAwait(false);
            }

            await _processorHub.UpdateProcessor.SetProcessedByForUnitsAsync(unitGroup, nameof(FriendHostedServiceProcessor)).ConfigureAwait(false);
        }
    }
}
