using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.DataAccess.Abstract
{
    public interface IUnitRepository
    {
        public Task CreateUnitAsync(Unit unit);
        public Task<ICollection<Unit>> GetUnitsAsync(UnitSearch? search = null);
        public Task<UserGroupedUnit?> AggregateUserGroupedUnitAsync(UnitSearch search);
        public Task SetProcessedByForUnitsAsync(UserGroupedUnit userGroupedUnit, string processorName);
    }
}
