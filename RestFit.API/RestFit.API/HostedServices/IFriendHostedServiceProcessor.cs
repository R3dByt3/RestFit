using RestFit.DataAccess.Abstract;

namespace RestFit.API.HostedServices
{
    public interface IFriendHostedServiceProcessor
    {
        public Task<UserGroupedUnit?> FetchNextAsync();
        public Task ProcessAsync(UserGroupedUnit unitGroup);
    }
}
