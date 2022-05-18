using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.API.Controllers.v1
{
    public interface IFriendControllerV1Processor
    {
        public Task CreateFriendRequestAsync(string username);
    }
}