using MongoDB.Driver;
using RestFit.Repository.Abstract;
using RestFit.Repository.Abstract.KnownFilters;
using RestFit.Repository.Abstract.KnownSearches;

namespace RestFit.Repository
{
    public class UnitRepository : IUnitRepository
    {
        private readonly IUnitAccess _unitAccess;

        public UnitRepository(IUnitAccess unitAccess)
        {
            _unitAccess = unitAccess;
        }

        public void Insert(Unit unit)
        {
            if (!string.IsNullOrWhiteSpace(unit.Id))
                throw new ArgumentException(nameof(unit.Id));
            if (string.IsNullOrWhiteSpace(unit.UserId))
                throw new ArgumentException(nameof(unit.UserId));
            if (string.IsNullOrWhiteSpace(unit.Type))
                throw new ArgumentException(nameof(unit.Type));
            _unitAccess.InsertDocument(unit);
        }

        public ICollection<Unit> GetUnits(UnitSearch? search = null)
        {
            var filter = BuildFilter(search);
            return _unitAccess.RetrieveDocuments(filter);
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
