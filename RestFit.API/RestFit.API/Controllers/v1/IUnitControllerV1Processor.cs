using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.API.Controllers.v1
{
    public interface IUnitControllerV1Processor
    {
        public Task<UnitDto> CreateUnitAsync(UnitDto unitDto);
        public Task<IEnumerable<UnitDto>> GetUnitsAsync(UnitSearchDto? searchDto = null);
    }
}