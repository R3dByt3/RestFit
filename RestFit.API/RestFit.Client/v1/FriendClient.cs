using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;
using RestFit.Client.Abstract.v1;
using RestFit.Client.Extensions;

namespace RestFit.Client.v1
{
    public class FriendClient : ClientBase, IFriendClient
    {
        protected override string BaseUrl => "Friend";
        
        public FriendClient(string username, string password) : base(username, password)
        {
        }

        public async Task CreateFriendRequestAsync(string username)
        {
            await ExecutePostAsync($"request/{username}").ConfigureAwait(false);
        }

        public async Task AcceptFriendRequestAsync(string userId)
        {
            await ExecutePostAsync($"accept/{userId}").ConfigureAwait(false);
        }

        public async Task DeclineFriendRequestAsync(string userId)
        {
            await ExecutePostAsync($"decline/{userId}").ConfigureAwait(false);
        }

        public async Task<List<FriendDto>> GetFriendsAsync(FriendSearchDto? search = null)
        {
            return await ExecuteGetAsync<List<FriendDto>>(null, search.GetParameters()).ConfigureAwait(false);
        }
    }
}
