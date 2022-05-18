using RestFit.API.Exceptions;
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
                throw new UserNotFoundException($"Username [{username}] could not be found");

            var userId = GetCurrentUserId();

            search = new UserSearch
            {
                Id = userId
            };

            var requestingUser = await _processorHub.SearchProcessor.GetUserAsync(search).ConfigureAwait(false);

            if (requestingUser == null)
                throw new UserNotFoundException($"Current user could not be found; Id: [{userId}]");

            await _processorHub.InsertProcessor.CreateFriendRequestAsync(user, requestingUser).ConfigureAwait(false);
        }
    }
}
