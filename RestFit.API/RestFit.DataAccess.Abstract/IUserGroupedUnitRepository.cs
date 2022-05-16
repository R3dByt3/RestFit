using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.DataAccess.Abstract
{
    public interface IUserGroupedUnitRepository
    {
        Task CreateUserGroupedUnitAsync(UserGroupedUnit userGroupedUnit);
        Task<UserGroupedUnit?> GetUserGroupedUnitAsync(UserGroupedUnitSearch search);
        Task UpdateUserGroupedUnitAggregationsAsync(UserGroupedUnit userGroupedUnit);
    }
}
