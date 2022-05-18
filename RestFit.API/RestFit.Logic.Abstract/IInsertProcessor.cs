using RestFit.DataAccess.Abstract;

namespace RestFit.Logic.Abstract
{
    public interface IInsertProcessor
    {
        public Task CreateUnitAsync(Unit unit);
        public Task CreateUserAsync(User user);
        public Task CreateUserGroupedUnitAsync(UserGroupedUnit userGroupedUnit);
        public Task CreateFriendRequestAsync(User user, User requestingUser);
    }
}
