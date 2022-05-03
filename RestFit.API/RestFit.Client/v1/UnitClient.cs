using RestFit.Client.Abstract.Model;
using RestFit.Client.Abstract.v1;
using RestFit.Client.Extensions;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.Client.v1
{
    public class UnitClient : ClientBase, IUnitClient
    {
        protected override string BaseUrl => "Unit";

        public UnitClient(string username, string password) : base(username, password)
        {

        }

        public async Task<IList<UnitDto>> GetUnits(UnitSearchDto? search = null)
        {
            return await ExecuteGetAsync<List<UnitDto>>(null, search.GetParameters()).ConfigureAwait(false);
        }

        public async Task AddUnit(UnitDto unit)
        {
            await ExecutePostAsync(null, unit).ConfigureAwait(false);
        }
    }
}
