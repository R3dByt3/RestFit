using MongoDB.Driver;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.Exceptions;
using RestFit.DataAccess.KnownFilters;
using RestFit.DataAccess.Abstract.KnownSearches;

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
            await _unitAccess.InsertDocumentAsync(unit).ConfigureAwait(false);
        }

        public async Task<ICollection<Unit>> GetUnitsAsync(UnitSearch? search = null)
        {
            var filter = BuildFilter(search);
            return await _unitAccess.RetrieveDocumentsAsync(filter).ConfigureAwait(false);
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
                    _ => empty
                };
            }

            var filter = UnitFilters.Empty;
            return search == null ? filter : search.ToKeyValuePairs().Aggregate(filter, AddFilter);
        }
    }
}
