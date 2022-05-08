using RestFit.Client.Abstract.Model;

namespace RestFit.API.Controllers.v1
{
    public interface IUserControllerV1Processor
    {
        public Task CreateUserAsync(UserDto user);
        public Task<UserDto> GetMyUserAsync();
    }
}