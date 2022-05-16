namespace RestFit.API.HostedServices
{
    public class FriendHostedService : IHostedService, IDisposable
    {
        private readonly IFriendHostedServiceProcessor _processor;
        private readonly ILogger<FriendHostedService> _logger;
        private Timer _timer = null!;
        private bool disposedValue;

        public FriendHostedService(ILoggerFactory loggerFactory, IFriendHostedServiceProcessor processor)
        {
            _processor = processor;
            _logger = loggerFactory.CreateLogger<FriendHostedService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(FriendHostedService)} starting");
            _timer = new Timer(async _ => await ExecuteSafe().ConfigureAwait(false), cancellationToken, TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(30));
            _logger.LogInformation($"{nameof(FriendHostedService)} started");

            return Task.CompletedTask;
        }

        private async Task ExecuteSafe()
        {
            try
            {
                await Execute().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(FriendHostedService)}.{nameof(Execute)} failed");
            }
        }

        private async Task Execute()
        {
            _logger.LogInformation($"{nameof(FriendHostedService)} executing");
            var item = await _processor.FetchNextAsync().ConfigureAwait(false);
            
            while (item != null)
            {
                await _processor.ProcessAsync(item).ConfigureAwait(false);
                item = await _processor.FetchNextAsync().ConfigureAwait(false);
            }
            _logger.LogInformation($"{nameof(FriendHostedService)} executed");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(FriendHostedService)} stopping");
            _timer.Change(0, Timeout.Infinite);
            _logger.LogInformation($"{nameof(FriendHostedService)} stopped");
            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _timer?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~FriendHostedService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
