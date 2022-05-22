using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.Logic.Abstract
{
    public interface ISearchProcessor
    {
        public Task<ICollection<Unit>> GetUnitsAsync(UnitSearch? search = null);
        public Task<Unit?> GetUnitAsync(UnitSearch? search = null);
        public Task<User?> GetUserAsync(UserSearch search);
        public Task<UserGroupedUnit?> AggregateUserGroupedUnitAsync(UnitSearch search);
        public Task<UserGroupedUnit?> GetUserGroupedUnitAsync(UserGroupedUnitSearch search);
        public Task<ICollection<UserGroupedUnit>> GetUserGroupedUnitsAsync(UserGroupedUnitSearch search);
        public Task<ICollection<HealthUnit>> GetHealthUnitsAsync(HealthUnitSearch search);
    }
}
