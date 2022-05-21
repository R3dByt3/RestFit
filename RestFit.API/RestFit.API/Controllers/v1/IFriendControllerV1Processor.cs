using RestFit.Client.Abstract.Model;

namespace RestFit.API.Controllers.v1
{
    public interface IFriendControllerV1Processor
    {
        public Task CreateFriendRequestAsync(string username);
        public Task<FriendDto> AcceptFriendRequest(string userId);
    }
}