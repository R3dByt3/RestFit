using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.DataAccess.Abstract
{
    public interface IFriendRepository
    {
        public Task CreateFriendAsync(Friend friend);
        public Task<ICollection<Friend>> GetFriendsAsync(FriendSearch? search = null);
    }
}
