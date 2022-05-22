using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;

namespace RestFit.API.Controllers.v1
{
    public interface IUnitControllerV1Processor
    {
        public Task<UnitDto> CreateUnitAsync(UnitDto unitDto);
        public Task<IEnumerable<UnitDto>> GetUnitsAsync(UnitSearchDto? searchDto = null);
    }
}