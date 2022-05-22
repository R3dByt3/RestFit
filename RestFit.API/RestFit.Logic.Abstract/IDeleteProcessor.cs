using RestFit.DataAccess.Abstract;

namespace RestFit.Logic.Abstract
{
    public interface IDeleteProcessor
    {
        public Task DeleteFriendRequestAsync(User user, User requestingUser);
    }
}
