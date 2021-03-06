using RestFit.API.Exceptions;
using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.Logic.Abstract;
using System.Security.Claims;

namespace RestFit.API.Controllers.v1
{
    public class FriendControllerV1Processor : IFriendControllerV1Processor
    {
        private readonly IProcessorHub _processorHub;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FriendControllerV1Processor(IProcessorHub processorHub, IHttpContextAccessor httpContextAccessor)
        {
            _processorHub = processorHub;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetCurrentUserId() => _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        public async Task CreateFriendRequestAsync(string username)
        {
            var search = new UserSearch
            {
                Username = username
            };
            var user = await _processorHub.SearchProcessor.GetUserAsync(search).ConfigureAwait(false);

            if (user == null)
                throw new UserNotFoundException($"User with username [{username}] could not be found");

            var userId = GetCurrentUserId();

            search = new UserSearch
            {
                Id = userId
            };

            var requestingUser = await _processorHub.SearchProcessor.GetUserAsync(search).ConfigureAwait(false);

            if (requestingUser == null)
                throw new UserNotFoundException($"Current user could not be found; Id: [{userId}]");

            await _processorHub.UpdateProcessor.CreateFriendRequestAsync(user, requestingUser).ConfigureAwait(false);
        }

        public async Task AcceptFriendRequestAsync(string userId)
        {
            var search = new UserSearch
            {
                Id = userId
            };
            var user = await _processorHub.SearchProcessor.GetUserAsync(search).ConfigureAwait(false);

            if (user == null)
                throw new UserNotFoundException($"User with userId [{userId}] could not be found");

            var currentUserId = GetCurrentUserId();

            search = new UserSearch
            {
                Id = currentUserId
            };
            var requestingUser = await _processorHub.SearchProcessor.GetUserAsync(search).ConfigureAwait(false);

            if (requestingUser == null)
                throw new UserNotFoundException($"Current user could not be found; Id: [{currentUserId}]");

            await _processorHub.DeleteProcessor.DeleteFriendRequestAsync(user, requestingUser).ConfigureAwait(false);
            await _processorHub.UpdateProcessor.CreateFriendsAsync(user, requestingUser).ConfigureAwait(false);
        }

        public async Task DeclineFriendRequestAsync(string userId)
        {
            var search = new UserSearch
            {
                Id = userId
            };
            var user = await _processorHub.SearchProcessor.GetUserAsync(search).ConfigureAwait(false);

            if (user == null)
                throw new UserNotFoundException($"User with userId [{userId}] could not be found");

            var currentUserId = GetCurrentUserId();

            search = new UserSearch
            {
                Id = currentUserId
            };
            var requestingUser = await _processorHub.SearchProcessor.GetUserAsync(search).ConfigureAwait(false);

            if (requestingUser == null)
                throw new UserNotFoundException($"Current user could not be found; Id: [{currentUserId}]");

            await _processorHub.DeleteProcessor.DeleteFriendRequestAsync(user, requestingUser).ConfigureAwait(false);
        }

        public async Task<List<FriendDto>> GetFriendsAsync(FriendSearchDto searchDto)
        {
            var currentUserId = GetCurrentUserId();

            var search = new UserSearch
            {
                Id = currentUserId
            };

            var requestingUser = await _processorHub.SearchProcessor.GetUserAsync(search).ConfigureAwait(false);

            if (requestingUser == null)
                throw new UserNotFoundException($"Current user could not be found; Id: [{currentUserId}]");

            if (searchDto.Ids?.Any(x => !requestingUser.FriendUserIds.Contains(x)) ?? false)
                throw new FriendsNotFoundException($"At least one requested friend is not a friend of the current user; Id: [{currentUserId}]");

            if (!(searchDto.Ids?.Any() ?? false))
                return new List<FriendDto>();

            var userGroupedUnitSearch = new UserGroupedUnitSearch
            {
                UserIds = searchDto.Ids
            };

            var userGroupedUnits = await _processorHub.SearchProcessor.GetUserGroupedUnitsAsync(userGroupedUnitSearch).ConfigureAwait(false);

            var friends = new List<FriendDto>();

            foreach (var userGroupedUnit in userGroupedUnits.ToLookup(x => x.UserId))
            {
                var friendName = (await _processorHub.SearchProcessor.GetUserAsync(new UserSearch { Id = userGroupedUnit.Key }).ConfigureAwait(false))?.Username ?? string.Empty;

                if (friendName == string.Empty)
                    continue;

                friends.Add(new FriendDto
                {
                    UserId = currentUserId,
                    FriendId = userGroupedUnit.Key,
                    UnitAggregationDtos = userGroupedUnit.Select(x => new UnitAggregationDto
                    {
                        AverageRepitions = x.RepetitionsSum / x.DocumentCount,
                        AverageSets = x.SetsSum / x.DocumentCount,
                        AverageWeight = x.WeightsSum / x.DocumentCount,
                        Type = x.Type
                    }).ToArray(),
                    Name = friendName
                });
            }

            foreach(var missingUserId in searchDto.Ids?.Except(friends.Select(x => x.FriendId)) ?? Array.Empty<string>())
            {
                var friendName = (await _processorHub.SearchProcessor.GetUserAsync(new UserSearch { Id = missingUserId }).ConfigureAwait(false))?.Username ?? string.Empty;

                friends.Add(new FriendDto
                {
                    UserId = currentUserId,
                    FriendId = missingUserId,
                    Name = friendName,
                    UnitAggregationDtos = Array.Empty<UnitAggregationDto>()
                });
            }

            return friends;
        }
    }
}
