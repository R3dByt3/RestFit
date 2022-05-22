using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;
using RestFit.Client.Abstract.v1;
using RestFit.Client.Extensions;

namespace RestFit.Client.v1
{
    public class HealthUnitClient : ClientBase, IHealthUnitClient
    {
        protected override string BaseUrl => "HealthUnit";

        public HealthUnitClient(string username, string password) : base(username, password)
        {

        }

        public async Task<IList<HealthUnitDto>> GetHealthUnitsAsync(HealthUnitSearchDto? search = null)
        {
            return await ExecuteGetAsync<List<HealthUnitDto>>(null, search.GetParameters()).ConfigureAwait(false);
        }

        public async Task AddHealthUnitAsync(HealthUnitDto HealthUnit)
        {
            await ExecutePostAsync(null, HealthUnit).ConfigureAwait(false);
        }
    }
}
