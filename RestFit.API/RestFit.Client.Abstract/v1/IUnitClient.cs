using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.Client.Abstract.v1
{
    public interface IUnitClient
    {
        public Task<IList<UnitDto>> GetUnits(UnitSearchDto? search = null);
        public Task AddUnit(UnitDto unit);
    }
}
