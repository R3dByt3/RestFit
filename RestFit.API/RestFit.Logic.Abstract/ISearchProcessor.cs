using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.Logic.Abstract
{
    public interface ISearchProcessor
    {
        public Task<ICollection<Unit>> GetUnits(UnitSearch? search = null);
        public Task<User?> GetUserAsync(UserSearch search);
    }
}
