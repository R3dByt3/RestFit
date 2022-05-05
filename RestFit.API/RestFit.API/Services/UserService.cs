using RestFit.API.Extensions;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.Logic.Abstract;

namespace RestFit.API.Services
{
    public interface IUserService
    {
        Task<User?> Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly IProcessorHub _processorHub;

        public UserService(IProcessorHub processorHub)
        {
            _processorHub = processorHub;
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            var user = await _processorHub.SearchProcessor.GetUserAsync(new UserSearch { Username = username, Password = password }).ConfigureAwait(false);

            if (user == null)
                return null;

            return user.WithoutPassword();
        }
    }
}
