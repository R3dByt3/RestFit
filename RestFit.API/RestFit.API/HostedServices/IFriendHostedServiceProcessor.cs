using RestFit.DataAccess.Abstract;

namespace RestFit.API.HostedServices
{
    public interface IFriendHostedServiceProcessor
    {
        public Task<UnitGroup?> FetchNextAsync();
    }
}
