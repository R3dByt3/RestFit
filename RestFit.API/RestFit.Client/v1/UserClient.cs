using RestFit.Client.Abstract.Model;
using RestFit.Client.Abstract.v1;

namespace RestFit.Client.v1
{
    public class UserClient : ClientBase, IUserClient
    {
        protected override string BaseUrl => "User";

        public UserClient(string username, string password) : base(username, password)
        {
        }

        public async Task<UserDto> GetMyUser()
        {
            return await ExecuteGetAsync<UserDto>(null, null).ConfigureAwait(false);
        }
    }
}
