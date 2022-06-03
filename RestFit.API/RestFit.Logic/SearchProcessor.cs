using Microsoft.Extensions.Logging;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.Logic.Abstract;

namespace RestFit.Logic
{
    public class SearchProcessor : ISearchProcessor
    {
        private readonly IRepositoryHub _repositoryHub;
        private readonly ILogger _logger;

        public SearchProcessor(IRepositoryHub repositoryHub, ILoggerFactory loggerFactory)
        {
            _repositoryHub = repositoryHub;
            _logger = loggerFactory.CreateLogger<SearchProcessor>();
        }

        public async Task<ICollection<Unit>> GetUnitsAsync(UnitSearch? search = null)
        {
            _logger.LogInformation("Searching units");
            var result = await _repositoryHub.UnitRepository.GetUnitsAsync(search).ConfigureAwait(false);
            _logger.LogInformation("Successfully searched units");
            return result;
        }

        public async Task<Unit?> GetUnitAsync(UnitSearch? search = null)
        {
            _logger.LogInformation("Searching unit");
            var result = await _repositoryHub.UnitRepository.GetUnitsAsync(search).ConfigureAwait(false);
            _logger.LogInformation("Successfully searched unit");
            return result.FirstOrDefault();
        }

        public async Task<User?> GetUserAsync(UserSearch search)
        {
            _logger.LogInformation("Searching user");
            var result = await _repositoryHub.UserRepository.GetUsersAsync(search).ConfigureAwait(false);
            _logger.LogInformation("Successfully searched user");
            return result.FirstOrDefault();
        }

        public async Task<ICollection<User>> GetUsersAsync(UserSearch search)
        {
            _logger.LogInformation("Searching users");
            var result = await _repositoryHub.UserRepository.GetUsersAsync(search).ConfigureAwait(false);
            _logger.LogInformation("Successfully searched users");
            return result;
        }

        public async Task<UserGroupedUnit?> AggregateUserGroupedUnitAsync(UnitSearch search)
        {
            _logger.LogInformation("Searching unit group");
            var result = await _repositoryHub.UnitRepository.AggregateUserGroupedUnitAsync(search).ConfigureAwait(false);
            _logger.LogInformation("Successfully searched unit group");
            return result;
        }

        public async Task<UserGroupedUnit?> GetUserGroupedUnitAsync(UserGroupedUnitSearch search)
        {
            _logger.LogInformation("Searching user grouped unit");
            var result = await _repositoryHub.UserGroupedUnitRepository.GetUserGroupedUnitAsync(search).ConfigureAwait(false);
            _logger.LogInformation("Successfully searched user grouped unit");
            return result;
        }

        public async Task<ICollection<UserGroupedUnit>> GetUserGroupedUnitsAsync(UserGroupedUnitSearch search)
        {
            _logger.LogInformation("Searching user grouped units");
            var result = await _repositoryHub.UserGroupedUnitRepository.GetUserGroupedUnitsAsync(search).ConfigureAwait(false);
            _logger.LogInformation("Successfully searched user grouped units");
            return result;
        }

        public async Task<ICollection<HealthUnit>> GetHealthUnitsAsync(HealthUnitSearch search)
        {
            _logger.LogInformation("Searching health units");
            var result = await _repositoryHub.HealthUnitRepository.GetHealthUnitsAsync(search).ConfigureAwait(false);
            _logger.LogInformation("Successfully searched health units");
            return result;
        }
    }
}
