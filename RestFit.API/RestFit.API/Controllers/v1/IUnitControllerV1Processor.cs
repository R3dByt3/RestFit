using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.API.Controllers.v1
{
    public interface IUnitControllerV1Processor
    {
        public Task CreateUnitAsync(UnitDto unit);
        public Task<IEnumerable<UnitDto>> GetUnitsAsync(UnitSearchDto? search = null);
    }
}