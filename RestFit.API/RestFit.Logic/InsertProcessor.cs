using Microsoft.Extensions.Logging;
using RestFit.DataAccess.Abstract;
using RestFit.Logic.Abstract;

namespace RestFit.Logic
{
    public class InsertProcessor : IInsertProcessor
    {
        private readonly IRepositoryHub _repositoryHub;
        private readonly ILogger _logger;

        public InsertProcessor(IRepositoryHub repositoryHub, ILoggerFactory loggerFactory)
        {
            _repositoryHub = repositoryHub;
            _logger = loggerFactory.CreateLogger<InsertProcessor>();
        }

        public async Task CreateUnitAsync(Unit unit)
        {
            _logger.LogInformation("Create unit");
            await _repositoryHub.UnitRepository.CreateUnitAsync(unit).ConfigureAwait(false);
            _logger.LogInformation("Successfully created unit");
        }

        public async Task CreateUserAsync(User user)
        {
            _logger.LogInformation("Create user");
            await _repositoryHub.UserRepository.CreateUserAsync(user).ConfigureAwait(false);
            _logger.LogInformation("Successfully created user");
        }
    }
}
