using MongoDB.Driver;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.Exceptions;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.DataAccess.KnownFilters;

namespace RestFit.DataAccess
{
    public class UserGroupedUnitRepository : IUserGroupedUnitRepository
    {
        private readonly IUserGroupedUnitAccess _access;

        public UserGroupedUnitRepository(IUserGroupedUnitAccess access)
        {
            _access = access;
        }

        public async Task CreateUserGroupedUnitAsync(UserGroupedUnit userGroupedUnit)
        {
            if (string.IsNullOrWhiteSpace(userGroupedUnit.UserId))
                throw new InsufficientDataException($"{nameof(userGroupedUnit.UserId)} must be filled");

            await _access.InsertDocumentAsync(userGroupedUnit).ConfigureAwait(false);
        }

        public async Task<UserGroupedUnit?> GetUserGroupedUnitAsync(UserGroupedUnitSearch search)
        {
            var filter = BuildFilter(search);

            return await _access.RetrieveDocumentAsync(filter).ConfigureAwait(false);
        }

        private static FilterDefinition<UserGroupedUnit> BuildFilter(UserGroupedUnitSearch? search = null)
        {
            FilterDefinition<UserGroupedUnit> AddFilter(FilterDefinition<UserGroupedUnit> empty, KeyValuePair<UserGroupedUnitFields, string[]> pair)
            {
                return pair.Key switch
                {
                    UserGroupedUnitFields.UserId => empty & UserGroupedUnitFilters.GetByUserId(search.UserId),
                    _ => empty
                };
            }

            var filter = UserGroupedUnitFilters.Empty;
            return search == null ? filter : search.ToKeyValuePairs().Aggregate(filter, AddFilter);
        }
    }
}
