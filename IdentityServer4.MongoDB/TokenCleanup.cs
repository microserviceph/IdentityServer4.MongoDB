using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB
{
    public class TokenCleanup
    {
        private readonly TimeSpan _interval;
        private readonly IServiceProvider _serviceProvider;
        private ILogger<TokenCleanup> _logger;
        private CancellationTokenSource _source;

        public TokenCleanup(IServiceProvider serviceProvider, ILogger<TokenCleanup> logger, TokenCleanupOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _interval = TimeSpan.FromSeconds(options.Interval);
        }

        public void Start()
        {
            if (_source != null)
                throw new InvalidOperationException("Already started");

            _logger.LogDebug("Starting token cleanup");

            _source = new CancellationTokenSource();
            Task.Factory.StartNew(() => Start(_source.Token));
        }

        public void Stop()
        {
            _logger.LogDebug("Stopping token cleanup");

            _source?.Cancel();
            _source = null;
        }

        private async Task ClearTokens()
        {
            try
            {
                _logger.LogTrace("Querying for tokens to clear");

                using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetService<IOperationDbContext>();

                    var filter = Builders<Models.PersistedGrant>.Filter.Lt(x => x.Expiration, DateTime.UtcNow);

                    var expired = await dbContext.PersistedGrant.Find(filter).CountAsync();

                    _logger.LogDebug($"Clearing ${expired} tokens");

                    if (expired > 0)
                    {
                        await dbContext.PersistedGrant.DeleteManyAsync(filter);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception cleaning tokens ${ex.Message}");
            }
        }

        private async Task Start(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogDebug("CancellationRequested");
                    break;
                }

                try
                {
                    await Task.Delay(_interval, cancellationToken);
                }
                catch
                {
                    _logger.LogDebug("Task.Delay exception. exiting.");
                    break;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogDebug("CancellationRequested");
                    break;
                }

                await ClearTokens();
            }
        }
    }
}