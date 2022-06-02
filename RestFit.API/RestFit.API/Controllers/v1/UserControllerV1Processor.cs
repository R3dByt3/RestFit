using RestFit.API.Controllers.v1.Mappers;
using RestFit.API.Exceptions;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.KnownSearches;
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
            var currentUserId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var user = await _processorHub.SearchProcessor.GetUserAsync(new UserSearch { Id = currentUserId }).ConfigureAwait(false);

            if (user == null)
                throw new UserNotFoundException($"Current user could not be found; Id: [{currentUserId}]");

            return new UserDto
            {
                Id = currentUserId,
                Username = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Name)!.Value,
                FriendUserIds = user.FriendUserIds,
                PendingInFriendRequestUserIds = user.PendingInFriendRequestUserIds,
                PendingOutFriendRequestUserIds = user.PendingOutFriendRequestUserIds
            };
        }
    }
}
