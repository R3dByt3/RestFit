using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;

namespace RestFit.Client.Abstract.v1
{
    public interface IFriendClient
    {
        public Task AcceptFriendRequestAsync(string userId);
        public Task CreateFriendRequestAsync(string username);
        public Task DeclineFriendRequestAsync(string userId);
        public Task<List<FriendDto>> GetFriendsAsync(FriendSearchDto? search = null);
    }
}
