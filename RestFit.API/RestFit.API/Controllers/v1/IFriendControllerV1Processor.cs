using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;

namespace RestFit.API.Controllers.v1
{
    public interface IFriendControllerV1Processor
    {
        public Task CreateFriendRequestAsync(string username);
        public Task AcceptFriendRequestAsync(string userId);
        public Task<List<FriendDto>> GetFriendsAsync(FriendSearchDto searchDto);
        public Task DeclineFriendRequestAsync(string userId);
    }
}