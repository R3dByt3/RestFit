using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;

namespace RestFit.API.Controllers.v1
{
    public interface IHealthUnitControllerV1Processor
    {
        public Task<HealthUnitDto> CreateHealthUnitAsync(HealthUnitDto HealthUnitDto);
        public Task<IEnumerable<HealthUnitDto>> GetHealthUnitsAsync(HealthUnitSearchDto? searchDto = null);
    }
}