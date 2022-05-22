using Microsoft.Extensions.Logging;
using RestFit.DataAccess.Abstract;
using RestFit.Logic.Abstract;

namespace RestFit.Logic
{
    public class DeleteProcessor : IDeleteProcessor
    {
        private readonly IRepositoryHub _repositoryHub;
        private readonly ILogger _logger;

        public DeleteProcessor(IRepositoryHub repositoryHub, ILoggerFactory loggerFactory)
        {
            _repositoryHub = repositoryHub;
            _logger = loggerFactory.CreateLogger<DeleteProcessor>();
        }

        public async Task DeleteFriendRequestAsync(User user, User requestingUser)
        {
            _logger.LogInformation("Delete friend requests for users");
            await _repositoryHub.UserRepository.DeleteFriendRequestAsync(user, requestingUser).ConfigureAwait(false);
            _logger.LogInformation("Successfully deleted friend requests for users");
        }
    }
}
