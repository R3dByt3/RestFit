using RestFit.Data;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserAccess _userAccess;

        public UserRepository(IUserAccess userAccess)
        {
            _userAccess = userAccess;
        }

        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await _userAccess.RetrieveDocumentsAsync().ConfigureAwait(false);
        }

        public async Task CreateUserAsync(User user)
        {
            await _userAccess.InsertDocumentAsync(user).ConfigureAwait(false);
        }
    }
}
