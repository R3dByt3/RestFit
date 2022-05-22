using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.DataAccess.Abstract
{
    public interface IUserGroupedUnitRepository
    {
        public Task CreateUserGroupedUnitAsync(UserGroupedUnit userGroupedUnit);
        public Task<UserGroupedUnit?> GetUserGroupedUnitAsync(UserGroupedUnitSearch search);
        public Task UpdateUserGroupedUnitAggregationsAsync(UserGroupedUnit userGroupedUnit);
        public Task<ICollection<UserGroupedUnit>> GetUserGroupedUnitsAsync(UserGroupedUnitSearch search);
    }
}
