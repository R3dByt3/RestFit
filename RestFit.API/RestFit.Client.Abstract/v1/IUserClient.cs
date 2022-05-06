using RestFit.Client.Abstract.Model;

namespace RestFit.Client.Abstract.v1
{
    public interface IUserClient
    {
        public Task<UserDto> GetMyUser();
    }
}
