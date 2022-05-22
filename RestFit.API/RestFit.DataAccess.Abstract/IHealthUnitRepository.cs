using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.DataAccess.Abstract
{
    public interface IHealthUnitRepository
    {
        public Task CreateHealthUnitAsync(HealthUnit HealthUnit);
        public Task<ICollection<HealthUnit>> GetHealthUnitsAsync(HealthUnitSearch? search = null);
    }
}
