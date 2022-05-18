using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.DataAccess.Abstract
{
    public interface IUserRepository
    {
        public Task<ICollection<User>> GetUsersAsync(UserSearch? search = null);
        public Task CreateUserAsync(User user);
        public Task CreateFriendRequestAsync(User user, User requestingUser);
    }
}
