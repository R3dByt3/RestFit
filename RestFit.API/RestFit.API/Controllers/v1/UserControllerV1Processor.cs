using RestFit.API.Controllers.v1.Mappers;
using RestFit.Client.Abstract.Model;
using RestFit.Logic.Abstract;
using System.Security.Claims;

namespace RestFit.API.Controllers.v1
{
    public class UserControllerV1Processor : IUserControllerV1Processor
    {
        private readonly IProcessorHub _processorHub;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserControllerV1Processor(IProcessorHub processorHub, IHttpContextAccessor httpContextAccessor)
        {
            _processorHub = processorHub;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateUserAsync(UserDto user)
        {
            await _processorHub.InsertProcessor.CreateUserAsync(UserDtoMapper.Instance.Convert(user)).ConfigureAwait(false);
        }

        public async Task<UserDto> GetMyUserAsync()
        {
            await Task.Yield();

            return new UserDto
            {
                Id = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                Username = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Name)!.Value
            };
        }
    }
}
