using RestFit.API.Controllers.v1.Mappers;
using RestFit.API.Exceptions;
using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.Logic.Abstract;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
            using var sha512 = SHA512.Create();
            var bytes = Encoding.UTF8.GetBytes(user.Password);
            using var stream = new MemoryStream(bytes);
            user = user with { Password = Encoding.UTF8.GetString(await sha512.ComputeHashAsync(stream).ConfigureAwait(false)) };
            await _processorHub.InsertProcessor.CreateUserAsync(UserDtoMapper.Instance.Convert(user))
                .ConfigureAwait(false);
        }

        public async Task<UserDto> GetMyUserAsync()
        {
            var currentUserId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var user = await _processorHub.SearchProcessor.GetUserAsync(new UserSearch { Id = currentUserId })
                .ConfigureAwait(false);

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

        public async Task<List<UserDto>> GetUsersAsync(UserSearchDto searchDto)
        {
            var currentUserId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var requestingUser = await _processorHub.SearchProcessor.GetUserAsync(new UserSearch { Id = currentUserId })
                .ConfigureAwait(false);

            if (requestingUser == null)
                throw new UserNotFoundException($"Current user could not be found; Id: [{currentUserId}]");

            if (searchDto.Ids?.Any(x =>
                    !requestingUser.FriendUserIds.Union(requestingUser.PendingInFriendRequestUserIds)
                        .Union(requestingUser.PendingOutFriendRequestUserIds).Contains(x)) ?? false)
                throw new FriendsNotFoundException(
                    $"At least one requested user is not known to the current user; Id: [{currentUserId}]");

            return (await _processorHub.SearchProcessor.GetUsersAsync(UserSearchDtoMapper.Instance.Convert(searchDto))
                .ConfigureAwait(false)).Select(x =>
                new UserDto
                {
                    Id = x.Id,
                    Username = x.Username,
                }).ToList();
        }
    }
}