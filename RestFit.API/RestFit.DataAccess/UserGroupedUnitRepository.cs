using MongoDB.Driver;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.Exceptions;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.DataAccess.KnownFilters;
using RestFit.DataAccess.KnownUpdates;

namespace RestFit.DataAccess
{
    public class UserGroupedUnitRepository : IUserGroupedUnitRepository
    {
        private readonly IUserGroupedUnitAccess _userGroupedUnitAccess;

        public UserGroupedUnitRepository(IUserGroupedUnitAccess userGroupedUnitAccess)
        {
            _userGroupedUnitAccess = userGroupedUnitAccess;
        }

        public async Task CreateUserGroupedUnitAsync(UserGroupedUnit userGroupedUnit)
        {
            if (string.IsNullOrWhiteSpace(userGroupedUnit.UserId))
                throw new InsufficientDataException($"{nameof(userGroupedUnit.UserId)} must be filled");

            await _userGroupedUnitAccess.InsertDocumentAsync(userGroupedUnit).ConfigureAwait(false);
        }

        public async Task<UserGroupedUnit?> GetUserGroupedUnitAsync(UserGroupedUnitSearch search)
        {
            var filter = BuildFilter(search);

            return await _userGroupedUnitAccess.RetrieveDocumentAsync(filter).ConfigureAwait(false);
        }

        public async Task UpdateUserGroupedUnitAggregationsAsync(UserGroupedUnit userGroupedUnit)
        {
            var filterSearch = new UserGroupedUnitSearch
            {
                UserId = userGroupedUnit.UserId,
                Type = userGroupedUnit.Type
            };
            var filter = BuildFilter(filterSearch);

            var update = UserGroupedUnitUpdates.UpdateAggregations(userGroupedUnit.RepetitionsSum, userGroupedUnit.DocumentCount, userGroupedUnit.SetsSum, userGroupedUnit.WeightsSum);

            await _userGroupedUnitAccess.UpdateAsync(filter, update).ConfigureAwait(false);
        }

        public async Task<ICollection<UserGroupedUnit>> GetUserGroupedUnitsAsync(UserGroupedUnitSearch search)
        {
            var filter = BuildFilter(search);

            return await _userGroupedUnitAccess.RetrieveDocumentsAsync(filter).ConfigureAwait(false);
        }

        private static FilterDefinition<UserGroupedUnit> BuildFilter(UserGroupedUnitSearch? search = null)
        {
            FilterDefinition<UserGroupedUnit> AddFilter(FilterDefinition<UserGroupedUnit> empty, KeyValuePair<UserGroupedUnitFields, string[]> pair)
            {
                return pair.Key switch
                {
                    UserGroupedUnitFields.UserId => empty & UserGroupedUnitFilters.GetByUserId(search.UserId),
                    UserGroupedUnitFields.UserIds => empty & UserGroupedUnitFilters.GetByUserIds(search.UserIds),
                    UserGroupedUnitFields.Type => empty & UserGroupedUnitFilters.GetByType(search.Type),
                    _ => empty
                };
            }

            var filter = UserGroupedUnitFilters.Empty;
            return search == null ? filter : search.ToKeyValuePairs().Aggregate(filter, AddFilter);
        }
    }
}
