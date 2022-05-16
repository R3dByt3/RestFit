using MongoDB.Driver;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.Exceptions;
using RestFit.DataAccess.KnownFilters;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.DataAccess.KnownUpdates;

namespace RestFit.DataAccess
{
    public class UnitRepository : IUnitRepository
    {
        private readonly IUnitAccess _unitAccess;

        public UnitRepository(IUnitAccess unitAccess)
        {
            _unitAccess = unitAccess;
        }

        public async Task CreateUnitAsync(Unit unit)
        {
            if (!string.IsNullOrWhiteSpace(unit.Id))
                throw new InsufficientDataException($"{nameof(unit.Id)} must be empty");
            if (string.IsNullOrWhiteSpace(unit.UserId))
                throw new InsufficientDataException($"{nameof(unit.UserId)} must be filled");
            if (string.IsNullOrWhiteSpace(unit.Type))
                throw new InsufficientDataException($"{nameof(unit.Type)} must be filled");
            if (unit.Repetitions == 0)
                throw new InsufficientDataException($"{nameof(unit.Repetitions)} must be filled");
            if (unit.Weight <= 0)
                throw new InsufficientDataException($"{nameof(unit.Weight)} must be filled");
            if (unit.Sets == 0)
                throw new InsufficientDataException($"{nameof(unit.Sets)} must be filled");
            await _unitAccess.InsertDocumentAsync(unit).ConfigureAwait(false);
        }

        public async Task<UserGroupedUnit?> AggregateUserGroupedUnitAsync(UnitSearch search)
        {
            if (string.IsNullOrWhiteSpace(search.NotProcessedBy))
                throw new InsufficientDataException($"{nameof(search.NotProcessedBy)} must be filled");

            var filter = BuildFilter(search);
            var hasUnits = await _unitAccess.ExistsAsync(filter);

            if (!hasUnits) return null;

            return await _unitAccess.GroupAsync(UnitFilters.GetUnitGroups(search.NotProcessedBy));
        }

        public async Task<ICollection<Unit>> GetUnitsAsync(UnitSearch? search = null)
        {
            var filter = BuildFilter(search);
            return await _unitAccess.RetrieveDocumentsAsync(filter).ConfigureAwait(false);
        }

        public async Task SetProcessedByForUnitsAsync(UserGroupedUnit userGroupedUnit, string processorName)
        {
            var filterSearch = new UnitSearch
            {
                Ids = userGroupedUnit.DocumentIds
            };
            var filter = BuildFilter(filterSearch);

            var update = UnitUpdates.SetProcessedByForUnits(processorName);

            await _unitAccess.UpdateAsync(filter, update).ConfigureAwait(false);
        }

        private static FilterDefinition<Unit> BuildFilter(UnitSearch? search = null)
        {
            FilterDefinition<Unit> AddFilter(FilterDefinition<Unit> empty, KeyValuePair<UnitFields, string[]> pair)
            {
                return pair.Key switch
                {
                    UnitFields.Id => empty & UnitFilters.GetById(search.Id),
                    UnitFields.UserId => empty & UnitFilters.GetByUserId(search.UserId),
                    UnitFields.Type => empty & UnitFilters.GetByType(search.Type),
                    UnitFields.NotProcessedBy => empty & UnitFilters.GetIfNotProcessedBy(search.NotProcessedBy),
                    UnitFields.Ids => empty & UnitFilters.GetIfNotProcessedBy(search.NotProcessedBy),
                    _ => empty
                };
            }

            var filter = UnitFilters.Empty;
            return search == null ? filter : search.ToKeyValuePairs().Aggregate(filter, AddFilter);
        }
    }
}
