using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.Logic.Abstract
{
    public interface ISearchProcessor
    {
        public Task<ICollection<Unit>> GetUnitsAsync(UnitSearch? search = null);
        public Task<Unit?> GetUnitAsync(UnitSearch? search = null);
        public Task<User?> GetUserAsync(UserSearch search);
        public Task<Friend?> GetFriendAsync(FriendSearch search);
        public Task<UnitGroup?> GetUnitGroupAsync(UnitSearch search);
    }
}
