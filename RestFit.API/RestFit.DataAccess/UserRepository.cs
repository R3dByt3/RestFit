using MongoDB.Driver;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.DataAccess.KnownFilters;

namespace RestFit.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserAccess _userAccess;

        public UserRepository(IUserAccess userAccess)
        {
            _userAccess = userAccess;
        }

        public async Task CreateUserAsync(User user)
        {
            await _userAccess.InsertDocumentAsync(user).ConfigureAwait(false);
        }

        public async Task<ICollection<User>> GetUsersAsync(UserSearch? search = null)
        {
            return await _userAccess.RetrieveDocumentsAsync(BuildFilter(search)).ConfigureAwait(false);
        }

        private static FilterDefinition<User> BuildFilter(UserSearch? search = null)
        {
            FilterDefinition<User> AddFilter(FilterDefinition<User> empty, KeyValuePair<UserFields, string[]> pair)
            {
                return pair.Key switch
                {
                    UserFields.Id => empty & UserFilters.GetById(search.Id),
                    UserFields.Username => empty & UserFilters.GetByUsername(search.Username),
                    UserFields.Password => empty & UserFilters.GetByPassword(search.Password),
                    _ => empty
                };
            }

            var filter = UserFilters.Empty;
            return search == null ? filter : search.ToKeyValuePairs().Aggregate(filter, AddFilter);
        }
    }
}
