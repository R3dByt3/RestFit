using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;
using RestFit.Client.Abstract.v1;
using RestFit.Client.Extensions;

namespace RestFit.Client.v1
{
    public class UnitClient : ClientBase, IUnitClient
    {
        protected override string BaseUrl => "Unit";

        public UnitClient(string username, string password) : base(username, password)
        {

        }

        public async Task<IList<UnitDto>> GetUnitsAsync(UnitSearchDto? search = null)
        {
            return await ExecuteGetAsync<List<UnitDto>>(null, search.GetParameters()).ConfigureAwait(false);
        }

        public async Task AddUnitAsync(UnitDto unit)
        {
            await ExecutePostAsync(null, unit).ConfigureAwait(false);
        }
    }
}
