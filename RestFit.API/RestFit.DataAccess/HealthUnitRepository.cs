using MongoDB.Driver;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.Exceptions;
using RestFit.DataAccess.KnownFilters;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.DataAccess.KnownUpdates;

namespace RestFit.DataAccess
{
    public class HealthUnitRepository : IHealthUnitRepository
    {
        private readonly IHealthUnitAccess _HealthUnitAccess;

        public HealthUnitRepository(IHealthUnitAccess HealthUnitAccess)
        {
            _HealthUnitAccess = HealthUnitAccess;
        }

        public async Task CreateHealthUnitAsync(HealthUnit HealthUnit)
        {
            if (!string.IsNullOrWhiteSpace(HealthUnit.Id))
                throw new InsufficientDataException($"{nameof(HealthUnit.Id)} must be empty");
            if (string.IsNullOrWhiteSpace(HealthUnit.UserId))
                throw new InsufficientDataException($"{nameof(HealthUnit.UserId)} must be filled");
            if (HealthUnit.Weight == 0)
                throw new InsufficientDataException($"{nameof(HealthUnit.Weight)} must be filled");
            if (HealthUnit.ArmSize <= 0)
                throw new InsufficientDataException($"{nameof(HealthUnit.ArmSize)} must be filled");
            if (HealthUnit.WaistSize == 0)
                throw new InsufficientDataException($"{nameof(HealthUnit.WaistSize)} must be filled");
            if (HealthUnit.ThightSize <= 0)
                throw new InsufficientDataException($"{nameof(HealthUnit.ThightSize)} must be filled");
            if (HealthUnit.HipSize == 0)
                throw new InsufficientDataException($"{nameof(HealthUnit.HipSize)} must be filled");
            await _HealthUnitAccess.InsertDocumentAsync(HealthUnit).ConfigureAwait(false);
        }

        public async Task<ICollection<HealthUnit>> GetHealthUnitsAsync(HealthUnitSearch? search = null)
        {
            var filter = BuildFilter(search);
            return await _HealthUnitAccess.RetrieveDocumentsAsync(filter).ConfigureAwait(false);
        }

        private static FilterDefinition<HealthUnit> BuildFilter(HealthUnitSearch? search = null)
        {
            FilterDefinition<HealthUnit> AddFilter(FilterDefinition<HealthUnit> empty, KeyValuePair<HealthUnitFields, string[]> pair)
            {
                return pair.Key switch
                {
                    HealthUnitFields.UserId => empty & HealthUnitFilters.GetByUserId(search.UserId),
                    _ => empty
                };
            }

            var filter = HealthUnitFilters.Empty;
            return search == null ? filter : search.ToKeyValuePairs().Aggregate(filter, AddFilter);
        }
    }
}
