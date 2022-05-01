using RestFit.API.Extensions;
using RestFit.Data;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Services
{
    public interface IUserService
    {
        Task<User?> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }

    public class UserService : IUserService
    {
        private readonly IUserAccess _userAccess;

        public UserService(IUserAccess userAccess)
        {
            _userAccess = userAccess;
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            var users = _userAccess.RetrieveDocuments();
            var user = await Task.Run(() => users.SingleOrDefault(x => x.Username == username && x.Password == password));

            if (user == null)
                return null;

            return user.WithoutPassword();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _userAccess.RetrieveDocuments());
        }
    }
}
