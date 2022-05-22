using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;

namespace RestFit.Client.Abstract.v1
{
    public interface IHealthUnitClient
    {
        public Task<IList<HealthUnitDto>> GetHealthUnitsAsync(HealthUnitSearchDto? search = null);
        public Task AddHealthUnitAsync(HealthUnitDto HealthUnit);
    }
}
