using Microsoft.Extensions.Logging;
using RestFit.DataAccess.Abstract;
using RestFit.Logic.Abstract;

namespace RestFit.Logic
{
    public class UpdateProcessor : IUpdateProcessor
    {
        private readonly IRepositoryHub _repositoryHub;
        private readonly ILogger<UpdateProcessor> _logger;

        public UpdateProcessor(IRepositoryHub repositoryHub, ILoggerFactory loggerFactory)
        {
            _repositoryHub = repositoryHub;
            _logger = loggerFactory.CreateLogger<UpdateProcessor>();
        }

        public async Task SetProcessedByForUnitsAsync(UserGroupedUnit userGroupedUnit, string processorName)
        {
            _logger.LogInformation("Updating units processed by");
            await _repositoryHub.UnitRepository.SetProcessedByForUnitsAsync(userGroupedUnit, processorName).ConfigureAwait(false);
            _logger.LogInformation("Updated units processed by");
        }

        public async Task UpdateUserGroupedUnitAggregations(UserGroupedUnit userGroupedUnit)
        {
            _logger.LogInformation("Updating user grouped unit aggregations");
            await _repositoryHub.UserGroupedUnitRepository.UpdateUserGroupedUnitAggregationsAsync(userGroupedUnit).ConfigureAwait(false);
            _logger.LogInformation("Updated user grouped unit aggregations");
        }

        public async Task CreateFriendRequestAsync(User user, User requestingUser)
        {
            _logger.LogInformation("Create friend requests for users");
            await _repositoryHub.UserRepository.CreateFriendRequestAsync(user, requestingUser).ConfigureAwait(false);
            _logger.LogInformation("Successfully createded friend requests for users");
        }

        public async Task CreateFriendsAsync(User user, User requestingUser)
        {
            _logger.LogInformation("Create friends for users");
            await _repositoryHub.UserRepository.CreateFriendsAsync(user, requestingUser).ConfigureAwait(false);
            _logger.LogInformation("Successfully createded friends for users");
        }
    }
}
