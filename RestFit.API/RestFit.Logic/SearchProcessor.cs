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

        public async Task<Friend?> GetFriendAsync(FriendSearch search)
        {
            _logger.LogInformation("Searching friend");
            var result = await _repositoryHub.FriendRepository.GetFriendsAsync(search).ConfigureAwait(false);
            _logger.LogInformation("Successfully searched friend");
            return result.FirstOrDefault();
        }

        public async Task<UserGroupedUnit?> GetUnitGroupAsync(UnitSearch search)
        {
            _logger.LogInformation("Searching unit group");
            var result = await _repositoryHub.UnitRepository.GetUnitGroupAsync(search).ConfigureAwait(false);
            _logger.LogInformation("Successfully searched unit group");
            return result;
        }
    }
}
