using RestFit.API.Controllers.v1.Mappers;
using RestFit.Client.Abstract.Model;
using RestFit.Logic.Abstract;

namespace RestFit.API.Controllers.v1
{
    public class UserControllerV1Processor : IUserControllerV1Processor
    {
        private readonly IProcessorHub _processorHub;

        public UserControllerV1Processor(IProcessorHub processorHub)
        {
            _processorHub = processorHub;
        }

        public async Task CreateUserAsync(UserDto user)
        {
            await _processorHub.InsertProcessor.CreateUserAsync(UserDtoMapper.Instance.Convert(user)).ConfigureAwait(false);
        }
    }
}
